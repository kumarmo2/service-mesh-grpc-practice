using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using HelloService;

namespace HelloClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var channel = GrpcChannel.ForAddress("http://localhost:8000");
            var client = new Greeter.GreeterClient(channel);

            for (var i = 0; true; i++)
            {

                var response = await client.SayHelloAsync(new HelloRequest
                {
                    Name = $"kumarmo2-{i}"
                });
                Console.WriteLine($"response from i = {i}: '{response.Message}'");
                await Task.Delay(TimeSpan.FromMilliseconds(5000));
            }
        }
    }
}
