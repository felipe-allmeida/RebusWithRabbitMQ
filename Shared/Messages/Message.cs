using System;

namespace Shared.Messages
{
    public abstract class Message
    {
        public Guid AggregateId { get; set; }
        public DateTime DateTime { get; set; }
        protected Message()
        {
            DateTime = DateTime.Now;
        }
    }
}
