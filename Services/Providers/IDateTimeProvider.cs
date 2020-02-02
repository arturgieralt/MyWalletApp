using System;

namespace MyWalletApp.Services.Providers
{
    public interface IDateTimeProvider
    {
          DateTime GetCurrentUtcTime { get; }
    }
}