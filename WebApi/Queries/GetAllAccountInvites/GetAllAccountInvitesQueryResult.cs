using System.Collections.Generic;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.WebApi.DTO.Results;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllAccountInvites
{
    public class GetAllAccountInvitesQueryResult
    {
        public QueryResultStatus Status {get; set;}
        public List<AccountInviteResult> AccountInvites { get; set;}
    }
}