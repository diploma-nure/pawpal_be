namespace Application.Interfaces;

public interface IApplicationDbContext
{
    User? User { get; set; }

    DbSet<User> Users { get; set; }

    DbSet<Pet> Pets { get; set; }

    DbSet<PetFeature> PetFeatures { get; set; }

    DbSet<PetLike> PetLikes { get; set; }

    DbSet<Survey> Surveys { get; set; }

    DbSet<Picture> Pictures { get; set; }

    DbSet<SurveyOwnerDetails> SurveysOwnerDetails { get; set; }

    DbSet<SurveyResidenceDetails> SurveysResidenceDetails { get; set; }

    DbSet<SurveyPetPreferences> SurveysPetPreferences { get; set; }

    DbSet<PetApplication> Applications { get; set; }

    DbSet<Meeting> Meetings { get; set; }

    DbSet<Comment> Comments { get; set; }

    IQueryable<TResult> SqlQueryRaw<TResult>(string sql, params object[] parameters);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
