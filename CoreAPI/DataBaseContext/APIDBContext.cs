using CoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNetCore.Entities;

namespace CoreAPI.DataBaseContext
{
    public class APIDBContext : DbContext
    {
        public APIDBContext(DbContextOptions<APIDBContext> options) : base(options) { }
        public DbSet<DepartmentEntity> Department {get;set;}
        public DbSet<EmployeeEntity> Employee { get; set; }

        //New below Code from Cursor

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Company).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).HasMaxLength(20).HasDefaultValue("User");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // RefreshToken entity configuration
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);
                entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ExpiresAt).IsRequired();
                entity.Property(e => e.IsRevoked).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // Update the relationship configuration
                entity.HasOne(e => e.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

    //New below Code from Cursor
    public class RefreshToken
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
