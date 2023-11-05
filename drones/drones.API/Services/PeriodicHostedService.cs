using drones.API.Utils;

namespace drones.API.Services
{
    public class PeriodicHostedService : BackgroundService
    {
        private readonly ILogger<PeriodicHostedService> _logger;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(10);
        private readonly IServiceScopeFactory _factory;

        public PeriodicHostedService(ILogger<PeriodicHostedService> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    using (var scope = _factory.CreateScope())
                    {
                        var periodicTaskLogService = scope.ServiceProvider.GetRequiredService<IPeriodicTaskLogService>();
                        ApiResponse logsApiResponse = await periodicTaskLogService.RPeriodicTaskLogs();
                        if (logsApiResponse.IsValid)
                        {
                            _logger.LogInformation((string)logsApiResponse.Result);
                        }
                        else
                        {
                            _logger.LogWarning(string.Join(" && ", logsApiResponse.Errors));
                        }
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogWarning(string.Format(MessageText.PERIODIC_TASK_EXCEPTION, ex.Message));
                }
            }
        }
    }
}
