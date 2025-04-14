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

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
