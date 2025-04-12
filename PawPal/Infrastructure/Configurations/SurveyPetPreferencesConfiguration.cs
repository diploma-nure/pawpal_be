namespace Infrastructure.Configurations;

public class SurveyPetPreferencesEntityConfiguration : IEntityTypeConfiguration<SurveyPetPreferences>
{
    public void Configure(EntityTypeBuilder<SurveyPetPreferences> builder)
    {
        builder.ToTable("surveys_pet_preferences");

        builder.Property(p => p.Id).HasColumnName("survey_pet_preferences_id").IsRequired();
        builder.Property(p => p.PreferredSpecies).HasColumnName("preferred_species").HasColumnType("jsonb").IsRequired();
        builder.Property(p => p.PreferredSizes).HasColumnName("preferred_sizes").HasColumnType("jsonb").IsRequired();
        builder.Property(p => p.PreferredAges).HasColumnName("preferred_ages").HasColumnType("jsonb").IsRequired();
        builder.Property(p => p.PreferredGenders).HasColumnName("preferred_genders").HasColumnType("jsonb").IsRequired();
        builder.Property(p => p.DesiredActivityLevel).HasColumnName("desired_activity_level").IsRequired();
        builder.Property(p => p.ReadyForSpecialNeedsPet).HasColumnName("ready_for_special_needs_pet").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasMany(p => p.DesiredFeatures)
            .WithMany(f => f.SurveysPetPreferences)
            .UsingEntity<Dictionary<string, object>>(
                 "surveys_pet_preferences_pet_features",
                 j => j
                     .HasOne<PetFeature>()
                     .WithMany()
                     .HasForeignKey("pet_feature_id")
                     .HasConstraintName("FK_surveys_pet_preferences_pet_features_pet_feature_id"),
                 j => j
                     .HasOne<SurveyPetPreferences>()
                     .WithMany()
                     .HasForeignKey("survey_pet_preferences_id")
                     .HasConstraintName("FK_surveys_pet_preferences_pet_features_survey_pet_preferences_id"),
                 j =>
                 {
                     j.HasKey("survey_pet_preferences_id", "pet_feature_id");
                 });

        builder.HasKey(p => p.Id).HasName("PK_surveys_pet_preferences");
    }
}
