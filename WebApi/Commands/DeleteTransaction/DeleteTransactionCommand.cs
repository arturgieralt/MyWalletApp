using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.DeleteTransaction
{
    public class DeleteTransactionCommand: IRequest<CommandResult>
    {
        public long Id {get; set;}
    }
}