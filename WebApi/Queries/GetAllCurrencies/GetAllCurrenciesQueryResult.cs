using System.Collections.Generic;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllCurrencies
{
    public class GetAllCurrenciesQueryResult
    {
        public QueryResultStatus Status {get; set;}

        public List<Currency> Currencies { get; set;}
    }
}