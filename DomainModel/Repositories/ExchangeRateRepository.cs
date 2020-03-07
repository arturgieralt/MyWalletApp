using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.Services.Providers;
using System;

namespace MyWalletApp.DomainModel.Repositories
{
    public class ExchangeRateRepository: IExchangeRateRepository
    {
        protected ApplicationDbContext _dbContext  {get; }
        protected IDateTimeProvider _dateTimeProvider {get; }
        protected IUserContextProvider _userContextProvider {get;}

        public ExchangeRateRepository(
            ApplicationDbContext dbContext, 
            IDateTimeProvider dateTimeProvider,
            IUserContextProvider userContextProvider)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userContextProvider = userContextProvider;
        }

        public async Task<Currency> GetCurrencyByCode(string code)
        {
            return await _dbContext
                .Currencies
                .SingleOrDefaultAsync(c => c.ShortName == code)
                .ConfigureAwait(false);
        }

        public async Task<bool> DoesExchangeRateExist(DateTime date, long currencyId, long baseCurrencyId)
        {
            return await _dbContext
                .ExchangeRates
                .AnyAsync(r => r.Date == date && r.BaseCurrencyId == baseCurrencyId && r.CurrencyId == currencyId)
                .ConfigureAwait(false);
        }

        public async Task<long> Save(ExchangeRate exchangeRate) 
        {
            await _dbContext
                    .AddAsync(exchangeRate)
                    .ConfigureAwait(false);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return exchangeRate.Id;
        }
        
    }
}