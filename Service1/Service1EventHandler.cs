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
    public class Service1EventHandler : IHandleMessages<SagaStartedEvent>
    {
        private readonly IBus _bus;

        public Service1EventHandler(IBus bus)
        {
            this._bus = bus;
        }

        public async Task Handle(SagaStartedEvent message)
        {
            await _bus.Send(new StartService1Command { AggregateId = message.AggregateId });
        }
    }
}
