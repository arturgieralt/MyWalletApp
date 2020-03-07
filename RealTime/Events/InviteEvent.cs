namespace MyWalletApp.RealTime.Events
{
    public class InviteEvent: BaseEvent
    {
        public long AccountId { get; set; }

        public string Message { get; set; }
    }
}