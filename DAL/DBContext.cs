using Entities.DBModels.AppModels;
using Entities.DBModels.AuthModels;
using Entities.DBModels.SharedModels;
using Entities.DBModels.ShopModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using BC = BCrypt.Net.BCrypt;

namespace DAL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        #region AuthModels

        public DbSet<SystemUser> SystemUsers { get; set; }

        #endregion

        #region AppModels

        public DbSet<AppAttachment> AppAttachments { get; set; }
        public DbSet<Branch> Branchs { get; set; }

        #endregion

        #region ShopModels

        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopGallery> ShopGalleries { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()
              .Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                _ = modelBuilder.Entity(
                    entityType.Name,
                    x =>
                    {
                        _ = x.Property("CreatedAt")
                            .HasDefaultValueSql("getutcdate()");
                    });
            }

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()
               .Where(t => t.ClrType.IsSubclassOf(typeof(AuditEntity))))
            {
                _ = modelBuilder.Entity(
                    entityType.Name,
                    x =>
                    {
                        _ = x.Property("LastModifiedAt")
                            .HasDefaultValueSql("getutcdate()");
                    });
            }

            _ = modelBuilder.Entity<SystemUser>().HasData(new SystemUser
            {
                Id = 1,
                UserName = "Developer",
                Password = BC.HashPassword("dev123456")
            });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default
        )
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken);
        }

        private void OnBeforeSaving()
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
            DateTime utcNow = DateTime.UtcNow;

            foreach (EntityEntry entry in entries)
            {
                if (entry.Entity is BaseEntity basetrackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                            break;

                        case EntityState.Added:
                            basetrackable.CreatedAt = utcNow;
                            break;
                    }
                }
                else if (entry.Entity is AuditEntity audittrackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            audittrackable.LastModifiedAt = utcNow;
                            entry.Property(nameof(AuditEntity.CreatedAt)).IsModified = false;
                            break;

                        case EntityState.Added:
                            audittrackable.CreatedAt = utcNow;
                            audittrackable.LastModifiedAt = utcNow;
                            break;
                    }
                }

            }
        }

    }
}