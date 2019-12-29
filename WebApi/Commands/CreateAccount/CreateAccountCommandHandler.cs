using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;
using MyWalletApp.WebApi.Mappers;
using CurrencyModel =  MyWalletApp.DomainModel.Models.Currency;
using CurrencyRequest =  MyWalletApp.WebApi.Models.Currency;

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
            var mappedCurrency = EnumMapper.Map<CurrencyRequest, CurrencyModel>(request.Currency);

            var account = new Account(request.Name, mappedCurrency);
            return await _accountRepository.Save(account);
        }


    }
}