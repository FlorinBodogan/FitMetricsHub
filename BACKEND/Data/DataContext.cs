using BACKEND.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class DataContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BMI> BMIs { get; set; }
        public DbSet<RMB> RMBs { get; set; }
        public DbSet<ArterialTension> ArterialTensions { get; set; }
        public DbSet<Cholesterol> Cholesterols { get; set; }
        public DbSet<Triglycerides> Triglycerides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<BMI>()
                .HasOne(u => u.User)
                .WithMany(b => b.BMIs)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RMB>()
                .HasOne(u => u.User)
                .WithMany(r => r.RMBs)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArterialTension>()
                .HasOne(u => u.User)
                .WithMany(a => a.ArterialTensions)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cholesterol>()
                .HasOne(u => u.User)
                .WithMany(c => c.Cholesterols)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Triglycerides>()
                .HasOne(u => u.User)
                .WithMany(t => t.Triglycerides)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}