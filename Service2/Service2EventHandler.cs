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
    public class Service2EventHandler : IHandleMessages<Service1CompletedEvent>
    {
        private readonly IBus _bus;

        public Service2EventHandler(IBus bus)
        {
            this._bus = bus;
        }

        public async Task Handle(Service1CompletedEvent message)
        {
            await _bus.Send(new StartService2Command { AggregateId = message.AggregateId });
        }
    }
}
