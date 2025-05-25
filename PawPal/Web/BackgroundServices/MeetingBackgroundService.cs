namespace Web.BackgroundServices;

public class MeetingBackgroundService(IServiceScopeFactory scopeFactory, ILogger<MeetingBackgroundService> logger) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    private readonly ILogger<MeetingBackgroundService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Executing update of meetings statuses");

            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            try
            {
                var count = await mediator.Send(new UpdateMeetingStatusesCommand(), stoppingToken);
                _logger.LogInformation("Updated {count} meetings", count);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while updating meeting statuses: {message}", ex.Message);
            }

            await Task.Delay(Constants.TimePeriods.MeetingStatusCheck, stoppingToken);
        }
    }
}
