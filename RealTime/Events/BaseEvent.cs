using System;

namespace MyWalletApp.RealTime.Events
{
    public class BaseEvent: EventArgs
    {
        public EventType Type { get; set; }
        public string ConsumerId { get; set; }
        public string CreatedById { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}