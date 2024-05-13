using RabbitMQ.Client;
using System.Text;

namespace RabbitMqProjectTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factotry = new();
            factotry.Uri = new Uri("amqp://guest:guest@localhost:5672");
            factotry.ClientProvidedName = "RABBIT Sender App";

            IConnection connextion = factotry.CreateConnection();

            IModel channel = connextion.CreateModel();

            string exchangeName = "DemoExchange";
            string routingKey = "demo-routing-key";
            string queueName = "DemoQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false,false, null); 
            channel.QueueBind(queueName,exchangeName, routingKey,null);
            for(int index = 1; index <= 10; index++) 
            {
                Console.WriteLine($"Sending Message N°{index}");
                byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Salut Rabbit!{index}");
                channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

            }
            channel.Close();
            connextion.Close();

        }
    }
}
