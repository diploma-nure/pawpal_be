namespace Infrastructure.Configurations;

public class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.Property(x => x.Id).HasColumnName("pet_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Gender).HasColumnName("gender").IsRequired();
        builder.Property(x => x.Size).HasColumnName("size").IsRequired();
        builder.Property(x => x.AgeMonths).HasColumnName("age_months").IsRequired();
        builder.Property(x => x.Breed).HasColumnName("breed");
        builder.Property(x => x.HasSpecialNeeds).HasColumnName("has_special_needs").IsRequired();
        builder.Property(x => x.Features).HasColumnName("features");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(x => x.Id).HasName("pk_pets");
    }
}
