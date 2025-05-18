namespace Infrastructure.Configurations;

public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");

        builder.Property(c => c.Id).HasColumnName("comment_id").IsRequired();
        builder.Property(c => c.Value).HasColumnName("value").IsRequired();
        builder.Property(c => c.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(c => c.PetId).HasColumnName("pet_id");
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(c => c.Id).HasName("PK_comments");
    }
}
