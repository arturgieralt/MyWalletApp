using System.Threading.Tasks;
using MyWalletApp.RealTime.Events;

namespace MyWalletApp.RealTime
{
    public interface IEventEmitter
    {
         Task EmitEvent(BaseEvent appEvent);
    }
}
