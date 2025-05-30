﻿namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public const string DefaultConnection = "DevelopmentDb";

    public User? User { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Pet> Pets { get; set; }

    public DbSet<PetFeature> PetFeatures { get; set; }

    public DbSet<PetLike> PetLikes { get; set; }

    public DbSet<Survey> Surveys { get; set; }

    public DbSet<Picture> Pictures { get; set; }

    public DbSet<SurveyOwnerDetails> SurveysOwnerDetails { get; set; }

    public DbSet<SurveyResidenceDetails> SurveysResidenceDetails { get; set; }

    public DbSet<SurveyPetPreferences> SurveysPetPreferences { get; set; }

    public DbSet<PetApplication> Applications { get; set; }

    public DbSet<Meeting> Meetings { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public IQueryable<TResult> SqlQueryRaw<TResult>(string sql, params object[] parameters)
        => Database.SqlQueryRaw<TResult>(sql, parameters);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
