using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RoutineMS_Core.Services;
using System;
using System.Text;
using System.Text.Json;

namespace RoutineMS_Infraestructure.Services
{
    public class PublisherService : IPublisherService
    {
        private IConnection connection { get; set; } = null;
        private IModel channel { get; set; } = null;

        private readonly ILogger<PublisherService> _logger;

        public PublisherService(ILogger<PublisherService> logger)
        {
            _logger = logger;

            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost"};

                connection = factory.CreateConnection();

                channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not create connection due to {ex.Message}");
            }
        }

        public void PublishEvent(object data, string exchange, string routingKey)
        {
            try
            {
                channel.ExchangeDeclare(exchange: exchange, type: "topic");

                var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));

                channel.BasicPublish(exchange:exchange, routingKey:routingKey, basicProperties: null, body:message);

                _logger.LogInformation($"Message sent with routing key {routingKey}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not send message with routing key {routingKey} due to {ex.Message}");
            }
        }
    }
}
