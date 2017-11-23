using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RESTXama.Models
{
    public partial class ProjectDBContext : DbContext
    {
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Userinfo> Userinfo { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
            : base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProjectDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.PictureId });

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PictureId).HasColumnName("PictureID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Picture)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Gallery)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gallery_Users");
                    
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.IngredientName });

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_Ingredients");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.IngredientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredients_Prices");
                    
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).IsUnicode(false);
                
                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prices_Users");
                
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).IsUnicode(false);
                
                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Userinfo)
                    .HasForeignKey<Userinfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Userinfo_Users");
                    
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
