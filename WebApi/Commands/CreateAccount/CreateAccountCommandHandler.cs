using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.CreateAccount
{
    public class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand, CommandResult>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            var account = new Account(request.Name, request.CurrencyId);
            var acountId =  await _accountRepository.Save(account);

            return new CommandResult(){Status = CommandResultStatus.Success, Message = "Created"};
        }


    }
}