namespace Infrastructure.Configurations;

public class SurveyEntityConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.ToTable("surveys");

        builder.Property(s => s.Id).HasColumnName("survey_id").IsRequired();
        builder.Property(s => s.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(u => u.OwnerDetailsId).HasColumnName("owner_details_id");
        builder.Property(u => u.ResidenceDetailsId).HasColumnName("residence_details_id");
        builder.Property(u => u.PetPreferencesId).HasColumnName("pet_preferences_id");
        builder.Property(s => s.VacationPetCarePlan).HasColumnName("vacation_pet_care_plan");
        builder.Property(s => s.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasOne(s => s.OwnerDetails).WithMany(o => o.Surveys);
        builder.HasOne(s => s.ResidenceDetails).WithMany(r => r.Surveys);
        builder.HasOne(s => s.PetPreferences).WithMany(p => p.Surveys);
        
        builder.HasKey(s => s.Id).HasName("PK_surveys");
    }
}
