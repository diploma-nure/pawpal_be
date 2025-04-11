namespace Infrastructure.Configurations;

public class PetFeatureEntityConfiguration : IEntityTypeConfiguration<PetFeature>
{
    public void Configure(EntityTypeBuilder<PetFeature> builder)
    {
        builder.ToTable("pet_features");

        builder.Property(p => p.Id).HasColumnName("pet_feature_id").IsRequired();
        builder.Property(p => p.Feature).HasColumnName("feature").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(p => p.Id).HasName("pk_pet_features");
    }
}
