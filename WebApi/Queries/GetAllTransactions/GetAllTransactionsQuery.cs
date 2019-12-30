using MediatR;

namespace MyWalletApp.WebApi.Queries.GetAllTransactions
{
    public class GetAllTransactionsQuery: IRequest<GetAllTransactionsQueryResult>
    {
        public long? AccountId {get; set;}
    }
}