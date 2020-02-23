using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.AcceptInvite
{
    public class AcceptInviteCommand: IRequest<CommandResult>
    {
        public long Id {get; set; }

    }
}