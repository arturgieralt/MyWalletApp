using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyWalletApp.RealTime.Events;

namespace MyWalletApp.RealTime
{
    public class EventEmitter: IEventEmitter
    {
        private readonly IHubContext<EventHub, IEventHub> _eventHub;

        public EventEmitter(IHubContext<EventHub, IEventHub> eventHub)
        {
            _eventHub = eventHub;
        }
        public async Task EmitEvent(BaseEvent appEvent) {
            await _eventHub.Clients.User(appEvent.ConsumerId).EventEmitted(appEvent);
        }
    }
}