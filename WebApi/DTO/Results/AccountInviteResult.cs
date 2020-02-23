namespace MyWalletApp.WebApi.DTO.Results
{
    public class AccountInviteResult
    {

        public long Id { get; set; }
        public UserResult Invited { get; set; }
        public UserResult InvitedBy { get; set; }
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public bool TransactionRead { get; set; }
        public bool TransactionWrite { get; set; }
        public bool AccountDelete { get; set; }
        public bool AccountWrite { get; set; }
    }
}