namespace Infrastructure.Configurations;

public class PetLikeEntityConfiguration : IEntityTypeConfiguration<PetLike>
{
    public void Configure(EntityTypeBuilder<PetLike> builder)
    {
        builder.ToTable("pet_likes");

        builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(p => p.PetId).HasColumnName("pet_id").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasOne(pl => pl.User).WithMany(u => u.PetLikes);
        builder.HasOne(pl => pl.Pet).WithMany(p => p.PetLikes);

        builder.HasKey(p => new { p.UserId, p.PetId }).HasName("PK_pet_likes");
    }
}
