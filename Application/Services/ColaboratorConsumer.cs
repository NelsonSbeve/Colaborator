using Application.Services;
using Domain.Model;
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
 
        public ColaboratorConsumer()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
 
            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            _channel.QueueBind(queue: "collaborator_queue",
                  exchange: "logs",
                  routingKey: string.Empty);
 
            Console.WriteLine(" [*] Waiting for messages.");
        }
 
        public void StartConsuming()
        {        
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
 
                Console.WriteLine($" [x] Received {message}");
 
                
            };
            _channel.BasicConsume(queue: "collaborator_queue",
                                autoAck: true,
                                consumer: consumer);
        }
}

