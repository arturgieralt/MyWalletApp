using System;
using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface IExchangeRateRepository
    {
       Task<Currency> GetCurrencyByCode(string code);
       Task<bool> DoesExchangeRateExist(DateTime date, long currencyId, long baseCurrencyId);
       Task<long> Save(ExchangeRate exchangeRate); 
    }
}