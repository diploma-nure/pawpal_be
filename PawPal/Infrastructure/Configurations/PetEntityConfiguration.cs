using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

public class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.Property(p => p.Id).HasColumnName("pet_id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").IsRequired();
        builder.Property(p => p.Species).HasColumnName("species").IsRequired();
        builder.Property(p => p.Gender).HasColumnName("gender").IsRequired();
        builder.Property(p => p.Size).HasColumnName("size").IsRequired();
        builder.Property(p => p.Age).HasColumnName("age").IsRequired();
        builder.Property(p => p.HasSpecialNeeds).HasColumnName("has_special_needs").IsRequired();
        builder.Property(p => p.Description).HasColumnName("description");
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasMany(p => p.Features)
            .WithMany(f => f.Pets)
            .UsingEntity<Dictionary<string, object>>(
                 "pets_pet_features",
                 j => j
                     .HasOne<PetFeature>()
                     .WithMany()
                     .HasForeignKey("pet_feature_id")
                     .HasConstraintName("FK_pets_pet_features_pet_feature_id"),
                 j => j
                     .HasOne<Pet>()
                     .WithMany()
                     .HasForeignKey("pet_id")
                     .HasConstraintName("FK_pets_pet_features_pet_id"),
                 j =>
                 {
                     j.HasKey("pet_id", "pet_feature_id");
                 });

        builder.HasMany(p => p.Pictures)
            .WithOne(p => p.Pet)
            .HasForeignKey(p => p.PetId);


        builder.HasKey(p => p.Id).HasName("PK_pets");
    }
}
