using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Services;

public class HealthCheckService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();

                var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();
                var result = await reviewService.GetListAsync(0, 0, 1);
                Console.WriteLine($"Health check result: {result.Items.Count} at {DateTimeOffset.UtcNow.DateTime}");
            }
            catch (Exception ex)
            {
                // Log the error instead of crashing
                Console.WriteLine($"Error in HealthCheckService: {ex.Message}");
            }
            
            await Task.Delay(TimeSpan.FromMinutes(18), stoppingToken);
        }
    }
}