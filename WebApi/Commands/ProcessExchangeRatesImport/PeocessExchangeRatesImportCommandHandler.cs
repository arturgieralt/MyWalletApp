using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.ExternalData;
using MyWalletApp.WebApi.Commands.AddExchangeRate;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.ProcessExchangeRatesImport
{
    public class ProcessExchangeRatesImportCommandHandler: IRequestHandler<ProcessExchangeRatesImportCommand, CommandResult>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly IMediator _mediator;

        public ProcessExchangeRatesImportCommandHandler(IExchangeRateRepository exchangeRateRepository, IMediator mediator)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(ProcessExchangeRatesImportCommand request, CancellationToken cancellationToken)
        {
            var baseCurrency = await _exchangeRateRepository.GetCurrencyByCode(request.BaseCurrencyShortName);

            foreach(var exchangeRateAtDay in request.Rates) 
            {
              await ProcessSingleDayData(exchangeRateAtDay, baseCurrency.Id);
            }

            return new CommandResult()
            {
                Status = CommandResultStatus.Success
            };
        }

        private async Task ProcessSingleDayData(KeyValuePair<string, CurrencyExchangeRatePair> exchangeRateAtDay, long baseCurrencyId) 
        {
                DateTime date;
                if(ParseDateString(exchangeRateAtDay.Key, out date)) 
                {
                    foreach(var currencyExchangeRate in exchangeRateAtDay.Value)
                    {
                    var currencyShortName = currencyExchangeRate.Key;
                    var rate = currencyExchangeRate.Value;

                    await _mediator.Send(new AddExchangeRateCommand()
                    {
                        CurrencyShortName = currencyShortName,
                        BaseCurrencyId = baseCurrencyId,
                        Date = date,
                        Rate = rate
                    });
                    }
                    
                }  
        }

        private bool ParseDateString(string dateString, out DateTime dateValue) 
        {
            return DateTime.TryParseExact(
                    dateString, 
                    "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, 
                    out dateValue);
        }

    }
}