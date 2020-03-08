using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel
{
    public static class CurrencyList
    {
        public static Currency[] Currencies = new[]{
                new Currency() {Id = 1, ShortName = "EUR", Name = "EURO"},
                new Currency() {Id = 2, ShortName = "PLN", Name = "Polish Zloty"},
                new Currency() {Id = 3, ShortName = "USD", Name = "American Dollar"}
        };
    }
}