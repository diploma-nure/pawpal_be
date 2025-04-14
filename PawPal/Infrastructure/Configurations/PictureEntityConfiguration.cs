namespace Infrastructure.Configurations;

public class PictureEntityConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.ToTable("pictures");

        builder.Property(p => p.Id).HasColumnName("picture_id").IsRequired();
        builder.Property(p => p.Source).HasColumnName("source").IsRequired();
        builder.Property(p => p.Url).HasColumnName("url").IsRequired();
        builder.Property(p => p.Path).HasColumnName("path");
        builder.Property(p => p.Order).HasColumnName("order").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("user_id");
        builder.Property(p => p.PetId).HasColumnName("pet_id");
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(p => p.Id).HasName("PK_pictures");
    }
}
