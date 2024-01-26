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


        modelBuilder
            .Entity<timeTable>()
            .Property(e => e.timeRange)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions())
                ,
                v => JsonSerializer.Deserialize<int[]>(v, new JsonSerializerOptions()) ?? new int[0]
            );

        modelBuilder
            .Entity<timeTable>()
            .Property(e => e.existingSelection)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions())
                ,
                v => JsonSerializer.Deserialize<Selection[]>(v, new JsonSerializerOptions()) ?? new Selection[0]
            );




    }

}