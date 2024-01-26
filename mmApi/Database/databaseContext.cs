using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mmApi.Model;
public class timeTableDb : DbContext
{
    public timeTableDb(DbContextOptions options) : base(options) { }
    public DbSet<timeTable> timeTables { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<timeTable>()
            .Property(e => e.dateSelection)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions())
                ,
                v => JsonSerializer.Deserialize<DateTime[]>(v, new JsonSerializerOptions()) ?? new DateTime[0]
                );
    }

}