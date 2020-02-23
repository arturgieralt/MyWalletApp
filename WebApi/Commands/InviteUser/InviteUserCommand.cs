using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.InviteUser
{
    public class InviteUserCommand: IRequest<CommandResult>
    {
        public string Email { get; set; }

        public long AccountId { get; set; }

        public bool TransactionRead { get; set; }

        public bool TransactionWrite { get; set; }

        public bool AccountDelete { get; set; }

        public bool AccountWrite { get; set; }

    }
}