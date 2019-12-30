using System.Collections.Generic;
using MyWalletApp.WebApi.Models;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllAcounts
{
    public class GetAllAccountsQueryResult
    {
        public QueryResultStatus Status {get; set;}

        public List<AccountSummary> Accounts { get; set;}
    }
}