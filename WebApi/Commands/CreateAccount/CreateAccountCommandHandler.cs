using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.CreateAccount
{
    public class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand, CommandResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContextProvider _userContextProvider;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IUserContextProvider userContextProvider)
        {
            _accountRepository = accountRepository;
            _userContextProvider = userContextProvider;
        }

        public async Task<CommandResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            
            var account = new Account(request.Name, request.CurrencyId);

            var acountId =  await _accountRepository.Save(account);
            var userId = _userContextProvider.GetUser.GetId<string>();
            var accountUsers = new List<AccountUser>{}; // should be method on Account // list should be private
            var accountUser = new AccountUser(userId, acountId);
            accountUsers.Add(accountUser);
            account.AccountUsers = accountUsers;
            await _accountRepository.Save(account);

            return new CommandResult(){Status = CommandResultStatus.Success, Message = "Created"};
        }


    }
}