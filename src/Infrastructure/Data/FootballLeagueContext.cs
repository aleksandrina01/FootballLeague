using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FootballLeagueContext : DbContext
    {
        public FootballLeagueContext(DbContextOptions<FootballLeagueContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Name).IsRequired();
                b.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<Match>(b =>
            {
                b.HasKey(m => m.Id);

                b.Property(m => m.FirstTeamScore).IsRequired(false);
                b.Property(m => m.SecondTeamScore).IsRequired(false);
                b.Property(m => m.PlayedAt).IsRequired(false);

                // Configure two FKs to the same principal table without cascade delete
                b.HasOne<Team>()
                    .WithMany()
                    .HasForeignKey(m => m.FirstTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<Team>()
                    .WithMany()
                    .HasForeignKey(m => m.SecondTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
