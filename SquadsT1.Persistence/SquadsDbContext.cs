using Microsoft.EntityFrameworkCore;
using SquadsT1.Persistence.Triggers;
using SquadsT1.Domain.Reservations;
using SquadsT1.Domain.Sessions;
using SquadsT1.Domain.Users;
using System.Reflection;

namespace SquadsT1.Persistence;

public class SquadsDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<SessionWeek> SessionWeeks => Set<SessionWeek>();
    public DbSet<SessionDay> SessionDays => Set<SessionDay>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TestDatabase;User Id=SA;Password=CosyRedKoalaDevelopment21!!;TrustServerCertificate=True");
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reservation>().HasKey(r => new { r.UserId, r.SessionId });
        modelBuilder.Entity<Reservation>()
            .HasOne<User>(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId);
        modelBuilder.Entity<Reservation>()
            .HasOne<Session>(r => r.Session)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SessionId);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}