using RabbitMQ.Client;
using System;
using System.Text;

namespace Vrum.BFF
{
    public class MessageBus : IMessageBus
    {
        const string EXCHANGE = "pending-rents";
        private ConnectionFactory _factory;
        public MessageBus()
        {
            _factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://oshgkrzg:UGeGl_ODOBs97UbTqKd00_CfN0oQRUsw@clam.rmq.cloudamqp.com/oshgkrzg")
            };
        }

        public void PostMessageTopic(string message, int userId)
        {
            string route = GetRoute(userId);
            IConnection connection = _factory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.ExchangeDeclare(EXCHANGE, "topic");

            byte[] body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: EXCHANGE, routingKey: route, basicProperties: null, body: body);
        }

        private string GetRoute(int userId) => $"user.rents.{userId}.key";
    }


    public interface IMessageBus
    {
        void PostMessageTopic(string aluguel, int userId);
    }
}