using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Application.Services
{
   
    public class ColaboratorPublisher
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _queueName;

        public ColaboratorPublisher(IConfiguration configuration)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"], // Ensure correct key name
                Port = int.Parse(configuration["RabbitMQ:Port"]), // Ensure correct key name
                UserName = configuration["RabbitMQ:UserName"], // Ensure correct key name
                Password = configuration["RabbitMQ:Password"]
            };
           _queueName = configuration["RabbitMQ:QueueName"] ?? throw new InvalidOperationException("Queue name is null. Please check configuration.");
        }

        public void PublishMessage(string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                    routingKey: _queueName,
                                    basicProperties: null,
                                    body: body);
            }
        }
    }
}