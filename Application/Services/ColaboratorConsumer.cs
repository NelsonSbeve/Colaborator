using System.Text;
using Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class ColaboratorConsumer : IColaboratorConsumer
{
    private readonly IModel _channel;
    private readonly string _queueName;

    public ColaboratorConsumer()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.ExchangeDeclare(exchange: "colab_logs", type: ExchangeType.Fanout);

        _queueName = "collaborator_queue";

        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: _queueName, exchange: "colab_logs", routingKey: "");
        Console.WriteLine(" [*] Waiting for Collaborator messages.");
    }

    public void StartConsuming()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [ColaboratorConsumer] {message}");
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }
}
