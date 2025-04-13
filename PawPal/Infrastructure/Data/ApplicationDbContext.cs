namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public const string DefaultConnection = "DevelopmentDb";

    public User? User { get; set; }

    public DbSet<TestEntity> TestEntities { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Pet> Pets { get; set; }

    public DbSet<PetFeature> PetFeatures { get; set; }

    public DbSet<PetLike> PetLikes { get; set; }

    public DbSet<Survey> Surveys { get; set; }

    public DbSet<SurveyOwnerDetails> SurveysOwnerDetails { get; set; }

    public DbSet<SurveyResidenceDetails> SurveysResidenceDetails { get; set; }

    public DbSet<SurveyPetPreferences> SurveysPetPreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => await base.SaveChangesAsync(cancellationToken);
}
