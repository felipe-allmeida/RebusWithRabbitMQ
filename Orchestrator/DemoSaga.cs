using Orchestrator.Commands;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using Shared.Messages.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator
{
    public class DemoSaga : Saga<DemoSagaData>,
        IAmInitiatedBy<StartSagaCommand>,
        IHandleMessages<Service1FinishedEvent>,
        IHandleMessages<Service2FinishedEvent>
    {
        private readonly IBus _bus;

        public DemoSaga(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<DemoSagaData> config)
        {
            config.Correlate<StartSagaCommand>(m => m.AggregateId, d => d.Id);
            config.Correlate<Service1FinishedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<Service2FinishedEvent>(m => m.AggregateId, d => d.Id);
        }

        public async Task Handle(StartSagaCommand message)
        {
            Console.WriteLine("Saga started!");

            await _bus.Publish(new SagaStartedEvent() { AggregateId = message.AggregateId });

            Data.SagaStarted = true;

            ProcessSaga();
        }

        public async Task Handle(Service1FinishedEvent message)
        {
            Console.WriteLine("Service 1 finished");

            await _bus.Publish(new Service1CompletedEvent { AggregateId = message.AggregateId });

            Data.Service1Finished = true;

            ProcessSaga();

        }

        public async Task Handle(Service2FinishedEvent message)
        {
            Console.WriteLine("Service 2 finished");            

            Data.Service2Finished = true;

            ProcessSaga();
        }

        private void ProcessSaga()
        {
            if (Data.IsSagaComplete)
            {
                Console.WriteLine("Saga complete!");
                MarkAsComplete();
            }
        }

    }
}
