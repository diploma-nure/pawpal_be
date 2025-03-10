namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TestEntity> TestEntities { get; set; }

    Task<int> SaveShangesAsync(CancellationToken cancellationToken);
}
