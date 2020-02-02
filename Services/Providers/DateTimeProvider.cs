using System;

namespace MyWalletApp.Services.Providers
{
    public class DateTimeProvider: IDateTimeProvider
    {
        public DateTime GetCurrentUtcTime => DateTime.UtcNow;
    }
}