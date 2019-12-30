using MediatR;
using MyWalletApp.WebApi.Commands.Common;
using MyWalletApp.WebApi.Models;

namespace MyWalletApp.WebApi.Commands.AddTransaction
{
    public class AddTransactionCommand: IRequest<CommandResult>
    {
        
        public string Name {get; set; }

        public long AccountId { get; set; }

        public decimal Total { get; set; }

        public TransactionType TransactionType { get; set; }

        public long? CategoryId { get; set; }
    }
}