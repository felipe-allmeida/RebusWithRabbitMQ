using Rebus.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestrator
{
    public class DemoSagaData : SagaData
    {
        public bool SagaStarted { get; set; }
        public bool Service1Finished { get; set; }
        public bool Service2Finished { get; set; }
        public bool IsSagaComplete =>  SagaStarted && Service1Finished && Service2Finished;
    }
}
