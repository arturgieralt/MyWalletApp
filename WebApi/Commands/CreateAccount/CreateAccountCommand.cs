using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.CreateAccount
{
    public class CreateAccountCommand: IRequest<CommandResult>
    {
        public string Name {get; set;}
        public long CurrencyId { get; set; }
    }
}