using System.Collections.Generic;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryResult
    {
        public QueryResultStatus Status {get; set;}

        public List<Category> Categories { get; set;}
    }
}