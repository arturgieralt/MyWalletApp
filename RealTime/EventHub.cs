using Microsoft.AspNetCore.SignalR;
using MyWalletApp.RealTime.Events;

namespace MyWalletApp.RealTime
{
    public class EventHub: Hub
    {
        private string EventEmitted = "EVENT_EMITTED";
        private IEventEmitter _eventEmitter;

        public EventHub(IEventEmitter eventEmitter) {
            _eventEmitter = eventEmitter;
            _eventEmitter.Subscribe(EventHandler);
        }

        public  void EventHandler (object sender, BaseEvent appEvent) {
            Clients.All.SendAsync(EventEmitted, appEvent.ConsumerId, appEvent);
        }
    }
}