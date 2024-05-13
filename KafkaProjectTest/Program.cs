using Confluent.Kafka;
using Newtonsoft.Json;

namespace KafkaProjectTest
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
            };
          using  var producer = new ProducerBuilder<Null,string>(config).Build();
            try
            {
                string state;
                while ((state =Console.ReadLine()) !=null) 
                {
                    var response = producer.ProduceAsync("weather-topic",
                        new Message<Null, string> { Value = JsonConvert.SerializeObject(new Weather(state,70) )});

                }
                
            } catch (ProduceException<Null,string> exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
 