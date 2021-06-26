using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace HelloService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private static readonly List<Person> _persons = new List<Person>();
        public GreeterService(ILogger<GreeterService> logger)
        {
            Console.WriteLine(">> Constructor called");
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Name: {request.Name}");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<Person> CreatePerson(CreatePersonRequest request, ServerCallContext context)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var currentMaxId = _persons.Count > 0 ? _persons.Max(person => person.Id) : 0;
            var person = new Person
            {
                Id = currentMaxId + 1,
                Name = request.Name
            };
            _persons.Add(person);
            return Task.FromResult(person);
        }

        public override async Task GetAllPersons(Empty request, IServerStreamWriter<Person> responseStream, ServerCallContext context)
        {
            if (!_persons.Any())
            {
                return;
            }
            foreach (var person in _persons)
            {
                await responseStream.WriteAsync(person);
            }
        }
    }
}
