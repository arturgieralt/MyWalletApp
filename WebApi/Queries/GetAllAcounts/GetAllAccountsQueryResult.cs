using System.Collections.Generic;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllAcounts
{
    public class GetAllAccountsQueryResult
    {
        public QueryResultStatus Status {get; set;}

        public List<Account> Accounts { get; set;}
    }
}