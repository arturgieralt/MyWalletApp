using System;
using System.Collections.Generic;
using MediatR;
using MyWalletApp.WebApi.Commands.Common;
using MyWalletApp.WebApi.Models;

namespace MyWalletApp.WebApi.Commands.AddTransaction
{
    public class AddTransactionCommand: IRequest<CommandResult>
    {
        
        public string Name {get; set; }

        public long AccountId { get; set; }

        public DateTime Date {get; set;}

        public decimal Total { get; set; }

        public TransactionType TransactionType { get; set; }

        public IList<string> Tags {get; set;}

        public long? CategoryId { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
    }
}