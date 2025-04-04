﻿namespace Infrastructure.Configurations;

public class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.ToTable("test_entities");

        builder.Property(x => x.Id).HasColumnName("test_entity_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();

        builder.HasKey(x => x.Id).HasName("pk_test_entities");
    }
}
