using System.Threading.Tasks;
using MyWalletApp.RealTime.Events;

namespace MyWalletApp.RealTime
{
    public interface IEventHub
    {
         Task EventEmitted (BaseEvent appEvent);
    }
}