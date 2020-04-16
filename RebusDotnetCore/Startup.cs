using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orchestrator;
using Orchestrator.Commands;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;
using Service1;
using Service1.Commands;
using Service2;
using Service2.Commands;
using Shared.Messages;
using Shared.Messages.IntegrationEvents;

namespace RebusDotnetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //https://github.com/rebus-org/RebusSamples/blob/master/RabbitTopics/RabbitTopics/Program.cs
            services.AddControllers();

            var mqQueue = "queue.exg";

            services.AddRebus(configure => configure
                //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), mqQueue))
                .Transport(t => t.UseRabbitMq("amqp://localhost:5672", mqQueue))
                //.Subscriptions(s => s.StoreInMemory())
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<Message>(mqQueue)
                        .MapAssemblyOf<StartSagaCommand>(mqQueue)
                        .MapAssemblyOf<StartService1Command>(mqQueue)
                        .MapAssemblyOf<StartService2Command>(mqQueue);
                })
                .Sagas(s => s.StoreInMemory())
                .Options(o =>
                {
                    o.SetNumberOfWorkers(1);
                    o.SetMaxParallelism(1);
                    o.SetBusName("RebusDotnetCore Demo");
                })
            );

            services.AutoRegisterHandlersFromAssemblyOf<DemoSaga>();
            services.AutoRegisterHandlersFromAssemblyOf<Service1CommandHandler>();
            services.AutoRegisterHandlersFromAssemblyOf<Service2CommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.UseRebus(async bus =>
            {
                await bus.Subscribe<SagaStartedEvent>();
                await bus.Subscribe<Service1FinishedEvent>();
                await bus.Subscribe<Service1CompletedEvent>();
                await bus.Subscribe<Service2FinishedEvent>();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
