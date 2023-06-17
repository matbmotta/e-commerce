using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace e_commerce
{
    public class RabbitMQConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQConsumer()
        {
            var factory = new ConnectionFactory() 
            {
                HostName = "localhost",               
            }; // Substitua "localhost" pelo endereço do servidor RabbitMQ, se necessário
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "fila-suporte", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Mensagem recebida: {0}", message);
            };

            _channel.BasicConsume(queue: "fila-suporte", autoAck: true, consumer: consumer);
            Console.WriteLine("Aguardando mensagens...");
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
