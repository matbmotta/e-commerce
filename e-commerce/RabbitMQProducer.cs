using Microsoft.AspNetCore.Connections;
using System.Text;
using RabbitMQ.Client;

namespace e_commerce
{
    public class RabbitMQProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQProducer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; // Substitua "localhost" pelo endereço do servidor RabbitMQ, se necessário
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "fila-suporte", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "fila-suporte", basicProperties: null, body: body);
            Console.WriteLine("Mensagem enviada: {0}", message);
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
