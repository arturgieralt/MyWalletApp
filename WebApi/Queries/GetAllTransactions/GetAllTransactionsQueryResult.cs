using System.Collections.Generic;
using MyWalletApp.WebApi.Queries.Common;
using MyWalletApp.WebApi.Models;

namespace MyWalletApp.WebApi.Queries.GetAllTransactions
{
    public class GetAllTransactionsQueryResult
    {
        public QueryResultStatus Status {get; set;}

        public List<Transaction> Transactions { get; set;}
    }
}