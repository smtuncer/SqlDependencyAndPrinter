using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RestourantPrinter.Models
{
    public partial class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext()
        {
        }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {
        } 
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<OrderDetailProp> OrderDetailProps { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-RD7402J8;Database=RestaurantDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

           

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<OrderDetailProp>(entity =>
            {
                entity.HasKey(e => e.OrderDetailPropId);

                entity.ToTable("OrderDetailProp");

                entity.HasIndex(e => e.Id, "IX_OrderDetailProp_Id");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.OrderDetailProps)
                    .HasForeignKey(d => d.Id);
            });

            modelBuilder.Entity<OrderHeader>(entity =>
            {
                entity.ToTable("OrderHeader");

                entity.HasIndex(e => e.ApplicationUserId, "IX_OrderHeader_ApplicationUserId");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.District).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PaymentMethod).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.SummaryNote).IsRequired();

                entity.Property(e => e.Surname).IsRequired();

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.OrderHeaders)
                    .HasForeignKey(d => d.ApplicationUserId);
            });

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
