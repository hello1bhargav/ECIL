using Microsoft.EntityFrameworkCore;
using ECILWebApp.Models;

namespace ECILWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
            });

            // Configure Request entity
            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ITChampionName).HasMaxLength(100);
                entity.Property(e => e.ITChampionCode).HasMaxLength(50);
                entity.Property(e => e.ITChampionDivision).HasMaxLength(100);
            });

            // Seed initial users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "user1", Password = "pass123", Role = "NormalUser" },
                new User { Id = 2, Username = "champion1", Password = "pass456", Role = "ITChampion" },
                new User { Id = 3, Username = "hod1", Password = "pass789", Role = "HOD" }
            );

            // Seed initial requests
            modelBuilder.Entity<Request>().HasData(
                new Request 
                { 
                    Id = 1, 
                    Department = "HR", 
                    RequestDate = DateTime.Parse("2025-05-20"), 
                    Status = "Pending",
                    ITChampionName = "Alice Johnson",
                    ITChampionCode = "AJ123",
                    ITChampionDivision = "Software"
                },
                new Request 
                { 
                    Id = 2, 
                    Department = "Finance", 
                    RequestDate = DateTime.Parse("2025-05-18"), 
                    Status = "Approved",
                    ITChampionName = "Bob Smith",
                    ITChampionCode = "BS456",
                    ITChampionDivision = "Infrastructure"
                },
                new Request 
                { 
                    Id = 3, 
                    Department = "IT", 
                    RequestDate = DateTime.Parse("2025-05-19"), 
                    Status = "Declined",
                    ITChampionName = "Clara Lee",
                    ITChampionCode = "CL789",
                    ITChampionDivision = "Network"
                },
                new Request 
                { 
                    Id = 4, 
                    Department = "Sales", 
                    RequestDate = DateTime.Parse("2025-05-17"), 
                    Status = "Pending",
                    ITChampionName = "David Brown",
                    ITChampionCode = "DB012",
                    ITChampionDivision = "Support"
                }
            );
        }
    }
}