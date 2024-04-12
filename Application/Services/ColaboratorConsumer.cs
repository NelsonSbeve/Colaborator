using Application.Services;
using Domain.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

public class ColaboratorConsumer : IColaboratorConsumer
{
     

        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string queueName;
 
        public ColaboratorConsumer()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
             _connection = factory.CreateConnection();
             _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");
            Console.WriteLine(" [*] Waiting for logs.");
        }
 
        public void StartConsuming()
        {        
           var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] {message}");
            };

            _channel.BasicConsume(queue:queueName, autoAck: true, consumer: consumer);
        }
}

