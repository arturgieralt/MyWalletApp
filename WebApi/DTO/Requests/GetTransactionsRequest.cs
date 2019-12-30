using Newtonsoft.Json;

namespace MyWalletApp.WebApi.DTO.Requests
{
    public class GetTransactionsRequest
    {
        public long? AccountId { get; set; }
    }
}