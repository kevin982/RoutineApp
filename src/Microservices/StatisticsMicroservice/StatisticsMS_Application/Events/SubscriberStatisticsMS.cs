using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatisticsMS_Core.Models.Entities;
using StatisticsMS_Core.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StatisticsMS_Application.Events
{
    public class SubscriberStatisticsMS : BackgroundService
    {
        private readonly IServiceProvider _services;

        private readonly ILogger<SubscriberStatisticsMS> _logger;

        private IConnection _connection { get; set; }
        private IModel _channel { get; set; }
        private string _queueName { get; set; }

        public SubscriberStatisticsMS(IServiceProvider services, ILogger<SubscriberStatisticsMS> logger)
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
                var factory = new ConnectionFactory() { HostName = "localhost" };

                _connection = factory.CreateConnection();

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "SetDone", type: "topic");

                _queueName = _channel.QueueDeclare().QueueName;

                _channel.QueueBind(queue: _queueName, exchange: "SetDone", routingKey: "RoutineMS.SetDone");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while initializing our statisticsMs subscriber due to {ex.Message}");
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
                _logger.LogError($"Could not register set done because of {ex.Message}");
            }
        }

        private async Task Handle(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content)) throw new Exception("The content to register set done can not be null or empty");

                var request = JsonSerializer.Deserialize<Statistic>(content);

                using (var scope = _services.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    await repository.Statistics.CreateAsync(request);

                    await repository.CompleteAsync();

                    _logger.LogInformation($"Set done with exercise id {request.ExerciseId} has been registered in {DateTime.UtcNow.ToString()}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not update the exercise because of {ex.Message}");
            }
        }
    }
}
