namespace Infrastructure.Configurations;

public class MeetingEntityConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("meetings");

        builder.Property(m => m.Id).HasColumnName("meeting_id").IsRequired();
        builder.Property(m => m.Start).HasColumnName("start").IsRequired();
        builder.Property(m => m.End).HasColumnName("end").IsRequired();
        builder.Property(m => m.Status).HasColumnName("status").IsRequired();
        builder.Property(m => m.AdminId).HasColumnName("admin_id").IsRequired();
        builder.Property(m => m.ApplicationId).HasColumnName("application_id").IsRequired();
        builder.Property(m => m.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(m => m.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasKey(m => m.Id).HasName("PK_meetings");
    }
}
