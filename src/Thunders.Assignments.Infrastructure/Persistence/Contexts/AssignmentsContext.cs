using Microsoft.EntityFrameworkCore;
using Thunders.Assignments.Infrastructure.Persistence.Configurations;

namespace Thunders.Assignments.Infrastructure.Persistence.Contexts;

internal class AssignmentsContext : DbContext
{
    private readonly string dbPath;

    public AssignmentsContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        dbPath = Path.Join(path, "blogging.db");
    }

    public DbSet<Domain.Entities.Assignment> Assignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
    }
}
