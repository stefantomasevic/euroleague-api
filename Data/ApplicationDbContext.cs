using Euroleague.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Euroleague.Data
{
    public class ApplicationDbContext : DbContext
    {

       

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
  


            modelBuilder.Entity<Team>()
            .HasMany(e => e.Players)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
            .HasOne(g => g.GuestTeam)
             .WithMany(t => t.GuestGames)
            .HasForeignKey(g => g.GuestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Statistic>()
            .HasOne(s => s.Player)
            .WithMany(p => p.Statistics)
            .HasForeignKey(s => s.PlayerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);


        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<Game> Games { get; set; }

    }
}
