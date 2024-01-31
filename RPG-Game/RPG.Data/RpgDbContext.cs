namespace RPG.Data;

using Microsoft.EntityFrameworkCore;

using Models;

using static Configuration;
using static Common.GameConstants.CharacterSelectConstants;

public class RpgDbContext : DbContext
{
    public RpgDbContext()
    {
    }

    public RpgDbContext(DbContextOptions options)
    :base(options)
    {
    }

    public DbSet<Race> Races { get; set; } = null!;

    public DbSet<RaceStat> RaceStats { get; set; } = null!;

    public DbSet<GameLog> GameLogs { get; set; } = null!;
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var entityTypes = builder.Model.GetEntityTypes().ToList();

        var foreignKeys = entityTypes
            .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        builder.Entity<Race>()
            .HasData(new HashSet<Race>
        {
            new Race
            {
                Id = WarriorMenuNumber,
                Name = "Warrior"
            },
            new Race
            {
                Id = ArcherMenuNumber,
                Name = "Archer"
            },
            new Race
            {
                Id = MageMenuNumber,
                Name = "Mage"
            }
        });

        base.OnModelCreating(builder);
    }
}