namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public const string DefaultConnection = "DevelopmentDb";

    public DbSet<TestEntity> TestEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task<int> SaveShangesAsync(CancellationToken cancellationToken)
        => await base.SaveChangesAsync(cancellationToken);
}
