using MediatR;
using MyWalletApp.WebApi.Commands.Common;
using MyWalletApp.WebApi.Models;

namespace MyWalletApp.WebApi.Commands.CreateAccount
{
    public class CreateAccountCommand: IRequest<CommandResult>
    {
        public string Name {get; set;}
        public Currency Currency { get; set; }
        public string UserId { get; set; }
    }
}