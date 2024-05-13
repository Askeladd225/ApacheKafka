using Confluent.Kafka;
using KafkaProjectTest;
using Newtonsoft.Json;
namespace KafKaProjectReceiverTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig()
            {
                GroupId = "weather-consume-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer =  new ConsumerBuilder<Null,string>(config).Build();
            consumer.Subscribe("weather-topic");

            CancellationTokenSource tokenSource = new();

            try
            {
            while(true)
                {
                    var response =consumer.Consume(tokenSource.Token);
                    if(response.Message!=null)
                    {
                        var weather = JsonConvert.DeserializeObject<Weather>(response.Message.Value);
                        Console.WriteLine($"State:{ weather.state}, Temp :{weather.temperature}C°");
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
