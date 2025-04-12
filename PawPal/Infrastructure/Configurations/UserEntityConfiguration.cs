namespace Infrastructure.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.Id).HasColumnName("user_id").IsRequired();
        builder.Property(u => u.SurveyId).HasColumnName("survey_id");
        builder.Property(u => u.Email).HasColumnName("email").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
        builder.Property(u => u.Role).HasColumnName("role").IsRequired();
        builder.Property(u => u.ProfilePictureUrl).HasColumnName("profile_picture_url");
        builder.Property(u => u.FullName).HasColumnName("full_name");
        builder.Property(u => u.PhoneNumber).HasColumnName("phone_number");
        builder.Property(u => u.Address).HasColumnName("address");
        builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasOne(u => u.Survey)
               .WithOne(s => s.User)
               .HasForeignKey<Survey>(s => s.UserId);

        builder.HasKey(u => u.Id).HasName("PK_users");
    }
}
