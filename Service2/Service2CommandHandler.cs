using Rebus.Bus;
using Rebus.Handlers;
using Service2.Commands;
using Shared.Messages.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service2
{
    public class Service2CommandHandler : IHandleMessages<StartService2Command>
    {
        private readonly IBus _bus;

        public Service2CommandHandler(IBus bus)
        {
            this._bus = bus;
        }

        public async Task Handle(StartService2Command message)
        {
            await _bus.Publish(new Service2FinishedEvent() { AggregateId = message.AggregateId });
        }
    }
}
