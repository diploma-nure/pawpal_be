namespace Infrastructure.Configurations;

public class PetApplicationEntityConfiguration : IEntityTypeConfiguration<PetApplication>
{
    public void Configure(EntityTypeBuilder<PetApplication> builder)
    {
        builder.ToTable("applications");

        builder.Property(a => a.Id).HasColumnName("application_id").IsRequired();
        builder.Property(a => a.Status).HasColumnName("status").IsRequired();
        builder.Property(a => a.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(a => a.PetId).HasColumnName("pet_id").IsRequired();
        builder.Property(a => a.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(a => a.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.Property(a => a.DeletedAt).HasColumnName("deleted_at");

        builder.HasOne(a => a.Meeting)
            .WithOne(m => m.Application)
            .HasForeignKey<Meeting>(a => a.ApplicationId);

        builder.HasKey(a => a.Id).HasName("PK_applications");
    }
}
