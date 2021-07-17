using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using HelloService;
using Consul;
using System.Net;
using System.Linq;
using Grpc.Core;

namespace HelloClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ConnectUsingEnvoy();
            // await ConnectUsingConsul();

            // var channel = GrpcChannel.ForAddress($"http://localhost:{6502}");
            // var invoker = channel.CreateCallInvoker();
            // channel.
        }


        static async Task ConnectUsingConsul()
        {
            using (var client = new ConsulClient())
            {
                var queryResult = await client.Catalog.Service("HelloService");
                if (queryResult == null)
                {
                    throw new Exception("queryResult was null");
                }
                if (!queryResult.StatusCode.IsSuccessResponse())
                {
                    throw new Exception("not successfull");
                }
                var services = queryResult.Response;
                if (services == null)
                {
                    throw new Exception("response was null");
                }
                if (!services.Any())
                {
                    throw new Exception("found no service");
                }
                var service = services[0];
                Console.WriteLine($"serviceAddress: {service.ServiceAddress}");
                Console.WriteLine($"address: {service.Address}");
                Console.WriteLine($"port: {service.ServicePort}");

                await CallHelloService(service.Address, service.ServicePort);


                // foreach (var service in services)
                // {
                //     var address = service.Address;
                //     Console.WriteLine($"address for service: {service.ServiceName}: {address}");
                // }
            }
        }

        static async Task CallHelloService(string address, int port)
        {
            Console.WriteLine("Hello World!");
            var channel = GrpcChannel.ForAddress($"http://{address}:{port}");
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


        static async Task ConnectUsingEnvoy()
        {
            Console.WriteLine("Hello World!");
            // var channel = GrpcChannel.ForAddress("http://localhost:8000");
            var channel = GrpcChannel.ForAddress("http://localhost:22000");
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

    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessResponse(this HttpStatusCode status)
        {
            int code = (int)status;
            return code >= 200 && code < 300;
        }
    }
}
