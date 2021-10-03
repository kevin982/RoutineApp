using ExerciseMS_Core.Models.Requests;
using ExerciseMS_Core.UoW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Events
{
    public class SubscriberExerciseMS : BackgroundService
    {
        private readonly IServiceProvider _services;
        
        private readonly ILogger<SubscriberExerciseMS> _logger;

        private IConnection _connection { get; set; }
        private IModel _channel { get; set; }
        private string _queueName { get; set; }

        public SubscriberExerciseMS(IServiceProvider services, ILogger<SubscriberExerciseMS> logger)
        {
            _services = services;
            _logger = logger;

            Initialize();

            _logger.LogInformation("Listening events!");
        }

        private void Initialize()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost"};

                _connection = factory.CreateConnection();   

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "UpdateExercise", type: "topic");

                _queueName = _channel.QueueDeclare().QueueName;

                _channel.QueueBind(queue: _queueName, exchange: "UpdateExercise", routingKey: "RoutineMS.UpdateExercise");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while initializing our exercisems subscriber due to {ex.Message}");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                stoppingToken.ThrowIfCancellationRequested();

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (ch, ea) =>
                {
                    var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                    await Handle(content);
                };

                _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not update the exercise because of {ex.Message}");
            }
        }

        private async Task Handle(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content)) throw new Exception("The content to update the exercise can not be null or empty");

                var request = JsonSerializer.Deserialize<UpdateExerciseRequest>(content);

                using(var scope = _services.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    await repository.Exercises.UpdateIsInTheRoutineAsync(request.NewValue, request.ExerciseId, request.UserId);

                    await repository.CompleteAsync();

                    _logger.LogInformation($"The exercise whose id is {request.ExerciseId} has been updated!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not update the exercise because of {ex.Message}");
            }
        }
    }


}
