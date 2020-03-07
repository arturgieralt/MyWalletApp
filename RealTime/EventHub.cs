using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MyWalletApp.Extensions;
using MyWalletApp.RealTime.Events;
using MyWalletApp.Services.Providers;

namespace MyWalletApp.RealTime
{
    [Authorize]
    public class EventHub: Hub<IEventHub>
    {

        public EventHub() {
        }

    }
}