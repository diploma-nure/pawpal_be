namespace Infrastructure.Configurations;

public class SurveyOwnerDetailsEntityConfiguration : IEntityTypeConfiguration<SurveyOwnerDetails>
{
    public void Configure(EntityTypeBuilder<SurveyOwnerDetails> builder)
    {
        builder.ToTable("surveys_owner_details");

        builder.Property(o => o.Id).HasColumnName("survey_owner_details_id").IsRequired();
        builder.Property(o => o.HasOwnnedPetsBefore).HasColumnName("has_owned_pets_before").IsRequired();
        builder.Property(o => o.UnderstandsResponsibility).HasColumnName("understands_responsibility").IsRequired();
        builder.Property(o => o.HasSufficientFinancialResources).HasColumnName("has_sufficient_financial_resources").IsRequired();
        builder.Property(o => o.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(o => o.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(o => o.Id).HasName("PK_surveys_owner_details");
    }
}
