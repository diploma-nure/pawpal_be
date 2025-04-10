﻿namespace Domain.Entities;

public class TestEntity : IAuditable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
