using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Karaoke_project.Models;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class web_karaokeContext : DbContext
    {
        public web_karaokeContext()
        {
        }

        public web_karaokeContext(DbContextOptions<web_karaokeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\Thai;Initial Catalog=web_karaoke;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheckIn).HasColumnName("checkIn");

                entity.Property(e => e.CheckOut).HasColumnName("checkOut");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt");

                entity.Property(e => e.DateBook)
                    .HasColumnType("datetime")
                    .HasColumnName("dateBook");

                entity.Property(e => e.IdCus).HasColumnName("idCus");

                entity.Property(e => e.IdRoom).HasColumnName("idRoom");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.IdCusNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.IdCus)
                    .HasConstraintName("FK_bill_cus");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("FK_bill_room");
            });

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.ToTable("billDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdBill).HasColumnName("idBill");

                entity.Property(e => e.IdFood).HasColumnName("idFood");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdBillNavigation)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.IdBill)
                    .HasConstraintName("FK_billdetail_bill");

                entity.HasOne(d => d.IdFoodNavigation)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.IdFood)
                    .HasConstraintName("FK_billdetail_food");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("food");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK_food_category");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TypeRoom).HasColumnName("typeRoom");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength(true);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(100)
                    .HasColumnName("avatar");

                entity.Property(e => e.Hoten)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username")
                    .IsFixedLength(true);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("FK_user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Karaoke_project.Models.Login> Login { get; set; }
    }
}
