using CalorieTracker.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
           : base(options)
        {
            _httpContextAccessor=httpContextAccessor;
        }

        // DbSets 
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodMeasure> FoodMeasures { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealFood> MealFoods { get; set; }
        public DbSet<WeightLog> WeightLogs { get; set; }
        public DbSet<FavoriteFood> FavoriteFoods { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // Food → FoodMeasure
            builder.Entity<FoodMeasure>()
                .HasOne(fm => fm.Food)
                .WithMany(f => f.Measures)
                .HasForeignKey(fm => fm.FoodId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → Meal
            builder.Entity<Meal>()
                .HasOne(m => m.User)
                .WithMany(u => u.Meals)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Meal → MealFood
            builder.Entity<MealFood>()
                .HasOne(mf => mf.Meal)
                .WithMany(m => m.MealFoods)
                .HasForeignKey(mf => mf.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            // Food → MealFood
            builder.Entity<MealFood>()
                .HasOne(mf => mf.Food)
                .WithMany(f => f.MealFoods)
                .HasForeignKey(mf => mf.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            // User → WeightLog
            builder.Entity<WeightLog>()
                .HasOne(wl => wl.User)
                .WithMany(u => u.WeightLogs)
                .HasForeignKey(wl => wl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → FavoriteFood
            builder.Entity<FavoriteFood>()
                .HasOne(ff => ff.User)
                .WithMany(u => u.FavoriteFoods)
                .HasForeignKey(ff => ff.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Food → FavoriteFood
            builder.Entity<FavoriteFood>()
                .HasOne(ff => ff.Food)
                .WithMany(f => f.FavoriteFoods)
                .HasForeignKey(ff => ff.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAudit();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            ApplyAudit();
            return base.SaveChanges();
        }
        private void ApplyAudit()
        {
            var auditEntries = ChangeTracker.Entries<BaseModel>();
            var currentUserId = _httpContextAccessor.HttpContext?.User?
                .FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";

            foreach (var entry in auditEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedBy).CurrentValue = currentUserId;
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedBy).CurrentValue = currentUserId;
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
        }

    }
}