using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectSSMP.Models
{
    public partial class sspmContext : DbContext
    {
        public virtual DbSet<MenuAuthentication> MenuAuthentication { get; set; }
        public virtual DbSet<MenuGroup> MenuGroup { get; set; }
        public virtual DbSet<RunningNumber> RunningNumber { get; set; }
        public virtual DbSet<UserAssignGroup> UserAssignGroup { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserSspm> UserSspm { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=den1.mssql5.gear.host;Initial Catalog=sspm;Integrated Security=False;User ID=sspm;Password=Gi90MMTY!H_i;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
        }
        public sspmContext(DbContextOptions<sspmContext> options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuAuthentication>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.MenuId });

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasMaxLength(2);

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<MenuGroup>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.MenuIcon).HasMaxLength(50);

                entity.Property(e => e.MenuName).HasMaxLength(50);

                entity.Property(e => e.MenuUrl)
                    .HasColumnName("MenuURL")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<RunningNumber>(entity =>
            {
                entity.HasKey(e => e.Type);

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(6);
            });

            modelBuilder.Entity<UserAssignGroup>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupId)
                    .IsRequired()
                    .HasColumnName("GroupID")
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasMaxLength(2)
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupName).HasMaxLength(30);
            });

            modelBuilder.Entity<UserSspm>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserSSPM");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.JobResponsible).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(30);

                entity.Property(e => e.Status).HasColumnType("char(1)");

                entity.Property(e => e.UserCreateBy).HasMaxLength(30);

                entity.Property(e => e.UserCreateDate).HasColumnType("datetime");

                entity.Property(e => e.UserEditBy).HasMaxLength(30);

                entity.Property(e => e.UserEditDate).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(30);
            });
        }
    }
}
