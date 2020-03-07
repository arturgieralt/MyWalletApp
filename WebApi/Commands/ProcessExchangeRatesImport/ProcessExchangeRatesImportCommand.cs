using System.Collections.Generic;
using MediatR;
using MyWalletApp.ExternalData;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.ProcessExchangeRatesImport
{
    public class ProcessExchangeRatesImportCommand: IRequest<CommandResult>
    {
        public string BaseCurrencyShortName {get; set;}
        public  Dictionary<string, ExchangeRate> Rates { get; set; }
    }
}