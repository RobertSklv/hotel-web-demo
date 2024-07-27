using HotelWebDemo.Data;
using HotelWebDemo.Services.Indexing;

namespace HotelWebDemo.Services.Crons;

public class IndexerService : BackgroundService
{
    private readonly IServiceScopeFactory scopeFactory;

    public IndexerService(IServiceScopeFactory scopeFactory)
    {
        this.scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = scopeFactory.CreateScope();
        using AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        using PeriodicTimer timer = new(TimeSpan.FromMinutes(5));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                Index(db);

                await db.SaveChangesAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }
    }

    public void Index(AppDbContext db)
    {
        new HotelIndexer(db).Process();
    }
}
