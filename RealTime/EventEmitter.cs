using System;
using MyWalletApp.RealTime.Events;

namespace MyWalletApp.RealTime
{
    public class EventEmitter: IEventEmitter
    {
        private event EventHandler<BaseEvent> AppEvent;

        public void Publish(BaseEvent appEvent) {
            AppEvent.Invoke(this, appEvent);
        }

        public void Subscribe(EventHandler<BaseEvent> eventHandler) {
            AppEvent += eventHandler;
        }

         public void UnSubscribe(EventHandler<BaseEvent> eventHandler) {
            AppEvent -= eventHandler;
        }
    }
}