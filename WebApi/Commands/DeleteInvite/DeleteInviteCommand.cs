using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.DeleteInvite
{
    public class DeleteInviteCommand: IRequest<CommandResult>
    {
        public long Id {get; set;}

    }
}