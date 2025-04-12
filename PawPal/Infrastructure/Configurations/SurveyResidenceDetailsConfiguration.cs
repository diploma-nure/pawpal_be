namespace Infrastructure.Configurations;

public class SurveyResidenceDetailsEntityConfiguration : IEntityTypeConfiguration<SurveyResidenceDetails>
{
    public void Configure(EntityTypeBuilder<SurveyResidenceDetails> builder)
    {
        builder.ToTable("surveys_residence_details");

        builder.Property(r => r.Id).HasColumnName("survey_residence_details_id").IsRequired();
        builder.Property(r => r.PlaceOfResidence).HasColumnName("place_of_residence").IsRequired();
        builder.Property(r => r.HasSafeWalkingArea).HasColumnName("has_safe_walking_area").IsRequired();
        builder.Property(r => r.PetsAllowedAtResidence).HasColumnName("pets_allowed_at_residence").IsRequired();
        builder.Property(r => r.HasOtherPets).HasColumnName("has_other_pets").IsRequired();
        builder.Property(r => r.HasSmallChildren).HasColumnName("has_small_children").IsRequired();
        builder.Property(r => r.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(r => r.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(r => r.Id).HasName("PK_surveys_residence_details");
    }
}
