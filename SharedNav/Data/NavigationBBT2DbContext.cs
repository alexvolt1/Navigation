using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SharedNav.Models;

namespace SharedNav.Data
{
    public partial class NavigationBBT2DbContext : DbContext
    {
        public NavigationBBT2DbContext()
        {
        }

        public NavigationBBT2DbContext(DbContextOptions<NavigationBBT2DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvailableDynamicProvider> AvailableDynamicProvider { get; set; }
        public virtual DbSet<DynamicProvider> DynamicProvider { get; set; }
        public virtual DbSet<NavigationGroup> NavigationGroup { get; set; }
        public virtual DbSet<NavigationItemAuthorization> NavigationItemAuthorization { get; set; }
        public virtual DbSet<NavigationItemBookmark> NavigationItemBookmark { get; set; }
        public virtual DbSet<NavigationItemLocale> NavigationItemLocale { get; set; }
        public virtual DbSet<NavigationItemNavigationItem> NavigationItemNavigationItem { get; set; }
        public virtual DbSet<NavigationItemRecent> NavigationItemRecent { get; set; }
        public virtual DbSet<NavigationItemViewSingle> NavigationItemViewSingle { get; set; }
        public virtual DbSet<NavigationItemView> NavigationItemView { get; set; }
        public virtual DbSet<NavigationItem> NavigationItem { get; set; }
        public virtual DbSet<NavigationMembership> NavigationMembership { get; set; }
        public virtual DbSet<SystemInfo> SystemInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<AvailableDynamicProvider>(entity =>
            {
                entity.HasKey(e => e.ClassName);

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .ValueGeneratedNever();

                entity.Property(e => e.AssemblyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.IsDynamic)
                    .IsRequired()
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<DynamicProvider>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.AssemblyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<NavigationGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Platform)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");
            });

            modelBuilder.Entity<NavigationItemAuthorization>(entity =>
            {
                entity.HasKey(e => new { e.NavigationItemTenantId, e.NavigationItemId, e.NavigationGroupId });

                entity.Property(e => e.NavigationItemTenantId).HasMaxLength(22);

                entity.Property(e => e.NavigationItemId).HasMaxLength(36);

                entity.Property(e => e.NavigationGroupId).HasMaxLength(36);

                entity.Property(e => e.NotInGroup)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('N')");
            });

            modelBuilder.Entity<NavigationItemBookmark>(entity =>
            {
                entity.HasKey(e => new { e.NavigationItemTenantId, e.UserId, e.NavigationItemId });

                entity.Property(e => e.NavigationItemTenantId)
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");

                entity.Property(e => e.UserId).HasMaxLength(255);

                entity.Property(e => e.NavigationItemId).HasMaxLength(36);
            });

            modelBuilder.Entity<NavigationItemLocale>(entity =>
            {
                entity.HasKey(e => new { e.NavigationItemTenantId, e.NavigationItemId, e.Locale });

                entity.Property(e => e.NavigationItemTenantId)
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");

                entity.Property(e => e.NavigationItemId).HasMaxLength(36);

                entity.Property(e => e.Locale).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<NavigationItemNavigationItem>(entity =>
            {
                entity.HasKey(e => new { e.NavigationItemTenantId, e.ParentId, e.NavigationItemId });

                entity.ToTable("NavigationItem_NavigationItems");

                entity.Property(e => e.NavigationItemTenantId)
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");

                entity.Property(e => e.ParentId).HasMaxLength(36);

                entity.Property(e => e.NavigationItemId).HasMaxLength(36);

                entity.Property(e => e.Inherited)
                    .IsRequired()
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<NavigationItemRecent>(entity =>
            {
                entity.HasKey(e => new { e.NavigationItemTenantId, e.UserId, e.NavigationItemId });

                entity.Property(e => e.NavigationItemTenantId)
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");

                entity.Property(e => e.UserId).HasMaxLength(255);

                entity.Property(e => e.NavigationItemId).HasMaxLength(36);

                entity.Property(e => e.DateViewed).HasColumnType("datetime");
            });

            modelBuilder.Entity<NavigationItemViewSingle>(entity =>
            {
                entity.ToTable("NavigationItem_View");

                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientID")
                    .HasMaxLength(3);

                entity.Property(e => e.Rtype)
                    .HasColumnName("RType")
                    .HasMaxLength(20);

                entity.Property(e => e.TopicId).HasMaxLength(36);

                entity.Property(e => e.ViewId).HasMaxLength(36);
            });

            modelBuilder.Entity<NavigationItemView>(entity =>
            {
                entity.HasKey(e => new { e.NavigationItemTenantId, e.UserId, e.NavigationItemId, e.DateViewed });

                entity.Property(e => e.NavigationItemTenantId)
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");

                entity.Property(e => e.UserId).HasMaxLength(255);

                entity.Property(e => e.NavigationItemId).HasMaxLength(36);

                entity.Property(e => e.DateViewed).HasColumnType("datetime");
            });

            modelBuilder.Entity<NavigationItem>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.DefaultDescription).HasMaxLength(2083);

                entity.Property(e => e.DefaultName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DocumentMode)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.IsMobile)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StoredType)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasMaxLength(22);
            });

            modelBuilder.Entity<NavigationMembership>(entity =>
            {
                entity.HasKey(e => new { e.NavigationGroupTenantId, e.NavigationGroupId, e.Member, e.Type });

                entity.Property(e => e.NavigationGroupTenantId)
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");

                entity.Property(e => e.NavigationGroupId).HasMaxLength(36);

                entity.Property(e => e.Member).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(10);
            });

            modelBuilder.Entity<SystemInfo>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.PropertyName });

                entity.Property(e => e.TenantId)
                    .HasMaxLength(22)
                    .HasDefaultValueSql("('infor')");

                entity.Property(e => e.PropertyName).HasMaxLength(255);
            });
        }
    }
}
