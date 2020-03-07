using System;
using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.AddExchangeRate
{
    public class AddExchangeRateCommand: IRequest<CommandResult>
    {
        public string CurrencyShortName {get; set;}
        public long BaseCurrencyId {get; set;}
        public DateTime Date {get; set;}
        public decimal Rate { get; set; }
    }
}