using RabbitMQ.Client;
using System;
using System.Text;

namespace Vrum.BFF
{
    public class MessageBus : IMessageBus
    {
        private ConnectionFactory _factory;
        public MessageBus()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost" //conferir ip com joao
            };
        }

        public void PostMessageTopic(string message)
        {
            string route = GetRoute(message);
            string exchange = GetExchange();
            IConnection connection = _factory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange, "topic");
            
            byte[] body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchange, routingKey: route, basicProperties: null, body: body);
            
            Console.WriteLine($"Mensagem enviada: {message}");
        }

        private string GetRoute(string message) => $"user-rents-{message}-key";
        private string GetExchange() => $"user-rents-queue"; // vai variar isso?
    }

    public interface IMessageBus
    {
        void PostMessageTopic(string message);
    }
}

/*

public void PostMessageQueue(string queueRoute, string message)

{
    IConnection connection = _factory.CreateConnection();
    IModel channel = connection.CreateModel();
    channel.QueueDeclare(queue: queueRoute, true, false, false, null);
            
    byte[] body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "", routingKey: queueRoute, basicProperties: null, body: body);
            
    Console.WriteLine($"Mensagem enviada: {message}");
}

*/