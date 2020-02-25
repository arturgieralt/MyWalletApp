using System;
using MyWalletApp.RealTime.Events;

namespace MyWalletApp.RealTime
{
    public interface IEventEmitter
    {
         void Publish(BaseEvent appEvent);
         void Subscribe(EventHandler<BaseEvent> eventHandler);
         void UnSubscribe(EventHandler<BaseEvent> eventHandler);
    }
}
