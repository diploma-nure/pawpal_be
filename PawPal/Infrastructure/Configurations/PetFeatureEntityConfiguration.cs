namespace Infrastructure.Configurations;

public class PetFeatureEntityConfiguration : IEntityTypeConfiguration<PetFeature>
{
    public void Configure(EntityTypeBuilder<PetFeature> builder)
    {
        builder.ToTable("pet_features");

        builder.Property(pf => pf.Id).HasColumnName("pet_feature_id").IsRequired();
        builder.Property(pf => pf.Feature).HasColumnName("feature").IsRequired();
        builder.Property(pf => pf.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(pf => pf.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(pf => pf.Id).HasName("pk_pet_features");
    }
}
