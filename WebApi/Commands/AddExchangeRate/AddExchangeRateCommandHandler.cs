using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.AddExchangeRate
{
    public class AddExchangeRateCommandHandler: IRequestHandler<AddExchangeRateCommand, CommandResult>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public AddExchangeRateCommandHandler(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<CommandResult> Handle(AddExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var currency = await _exchangeRateRepository.GetCurrencyByCode(request.CurrencyShortName);
            var doesExist = await _exchangeRateRepository.DoesExchangeRateExist(request.Date, currency.Id, request.BaseCurrencyId);

            if(doesExist) {
                return new CommandResult(){ Status = CommandResultStatus.Error, Message="Exchange rate exists." };
            }

            var exchangeRate = new ExchangeRate(){
                CurrencyId = currency.Id,
                BaseCurrencyId = request.BaseCurrencyId,
                Rate = request.Rate,
                Date = request.Date
            };

            await _exchangeRateRepository.Save(exchangeRate);
            return new CommandResult(){Status = CommandResultStatus.Success, Message = "Created"};
        }
    }
}