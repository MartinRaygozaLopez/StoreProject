using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreProject.Models.Models;

#nullable disable

namespace Store.DataAccess
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerProduct> CustomerProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Productstore> Productstores { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MXGDLM4IESQL81;Database=DBStore;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IDCustomer)
                    .HasName("ID_Customers");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.User).IsUnicode(false);

                entity.HasOne(d => d.FKRoleNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.FKRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_Roles");
            });

            modelBuilder.Entity<CustomerProduct>(entity =>
            {
                entity.HasKey(e => e.IDCustomerProduct)
                    .HasName("ID_CustomerProduct");

                entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKCustomerNavigation)
                    .WithMany(p => p.CustomerProducts)
                    .HasForeignKey(d => d.FKCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerProduct_Customer");

                entity.HasOne(d => d.FKProductNavigation)
                    .WithMany(p => p.CustomerProducts)
                    .HasForeignKey(d => d.FKProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerProduct_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IDProduct)
                    .HasName("ID_Products");

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Productstore>(entity =>
            {
                entity.HasKey(e => e.IDProductstore)
                    .HasName("ID_Productstore");

                entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FKProductNavigation)
                    .WithMany(p => p.Productstores)
                    .HasForeignKey(d => d.FKProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productstore_Products");

                entity.HasOne(d => d.FKStoreNavigation)
                    .WithMany(p => p.Productstores)
                    .HasForeignKey(d => d.FKStore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productstore_Stores");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IDRole)
                    .HasName("ID_Roles");

                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.IDStore)
                    .HasName("ID_Stores");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");

                entity.Property(e => e.Subsidiary).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
