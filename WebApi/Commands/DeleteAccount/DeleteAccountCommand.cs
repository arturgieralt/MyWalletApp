using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.DeleteAccount
{
    public class DeleteAccountCommand: IRequest<CommandResult>
    {
        public long Id {get; set;}
    }
}