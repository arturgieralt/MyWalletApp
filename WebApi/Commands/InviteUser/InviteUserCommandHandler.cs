using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.InviteUser
{
    public class InviteUserCommandHandler: IRequestHandler<InviteUserCommand, CommandResult>

    {
        public InviteUserCommandHandler(){

        }        

        public async Task<CommandResult> Handle (InviteUserCommand request, CancellationToken cancellationToken) {

        }

    }
}