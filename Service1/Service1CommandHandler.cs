using Rebus.Bus;
using Rebus.Handlers;
using Service1.Commands;
using Shared.Messages.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service1
{
    public class Service1CommandHandler : IHandleMessages<StartService1Command>
    {
        private readonly IBus _bus;

        public Service1CommandHandler(IBus bus)
        {
            this._bus = bus;
        }

        public async Task Handle(StartService1Command message)
        {
            await _bus.Publish(new Service1FinishedEvent() { AggregateId = message.AggregateId });
        }
    }
}
