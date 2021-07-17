using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Consul;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace HelloService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration _config;
        private string serviceId;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                // endpoints.MapHealthChecks("/health");

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Ok");
                });
            });
            // RegisterServiceInCluster();
            // applicationLifetime.ApplicationStarted.Register(OnStarted);
            // applicationLifetime.ApplicationStopping.Register(OnStopping);

            // Console.CancelKeyPress += (sender, eventArgs) =>
            // {
            //     applicationLifetime.StopApplication();
            //     eventArgs.Cancel = true;
            // };

        }

        private void OnStopping()
        {
            DeRegisterServiceInCluster().Wait();
        }
        private void OnStarted()
        {
            Console.WriteLine(">>>> On Started Called <<<<");
            RegisterServiceInCluster().Wait();
            // RegisterServiceInCluster();
        }

        private async Task DeRegisterServiceInCluster()
        {
            using (var client = new ConsulClient())
            {
                await client.Agent.ServiceDeregister(serviceId);
            }
        }

        private async Task RegisterServiceInCluster()
        {
            using (var client = new ConsulClient())
            {
                var serviceName = _config["service:name"];
                var id = $"{serviceName}-{Guid.NewGuid().ToString().Split('-', 2)[0]}";
                serviceId = id;
                var port = Convert.ToInt32(_config["service:port"]);


                var x = new AgentServiceRegistration
                {
                    Name = _config["service:name"],
                    ID = id,
                    Port = port,

                };
                await client.Agent.ServiceRegister(x);
            }
        }
    }
}
