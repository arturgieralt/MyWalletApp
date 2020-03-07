using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWalletApp.WebApi.Commands.ProcessExchangeRatesImport;

namespace MyWalletApp.ExternalData
{

    public class ExchangeRateHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<ExchangeRateHostedService> _logger;
    private IServiceProvider _serviceProvider;
    private CurrencyRateClient _apiClient;
    private Timer _timer;

    public ExchangeRateHostedService(ILogger<ExchangeRateHostedService> logger,  IServiceProvider serviceProvider,  CurrencyRateClient apiClient)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _apiClient = apiClient;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(async state => await DoWork(state), null, TimeSpan.Zero, 
            TimeSpan.FromHours(12));

        return Task.CompletedTask;
    }

    private async Task DoWork(object state)
    {
        var count = Interlocked.Increment(ref executionCount);
        var data = await _apiClient.GetRatesForTimePeriod(
            DateTime.Today.AddDays(-3), 
            DateTime.Today, 
            ExchangeRateConfig.BASE_CURRENCY_CODE,  
            ExchangeRateConfig.Currencies);
            
        using( var scope = _serviceProvider.CreateScope()) {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new ProcessExchangeRatesImportCommand()
            {
                BaseCurrencyShortName = ExchangeRateConfig.BASE_CURRENCY_CODE,
                Rates = data.Rates
            });
        }
        
        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
}