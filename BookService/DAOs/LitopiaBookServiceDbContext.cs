using System;
using System.Collections.Generic;
using BookService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookService.DAOs;

public partial class LitopiaBookServiceDbContext : DbContext
{
    public LitopiaBookServiceDbContext()
    {
    }

    public LitopiaBookServiceDbContext(DbContextOptions<LitopiaBookServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookLike> BookLikes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=THUC;Initial Catalog=Litopia_BookServiceDB;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207E782075B");

            entity.Property(e => e.BookId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.AverageRating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Books__CategoryI__3C69FB99");
        });

        modelBuilder.Entity<BookLike>(entity =>
        {
            entity.HasKey(e => e.BookLikeId).HasName("PK__BookLike__5E7163E627E85AE7");

            entity.Property(e => e.BookLikeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.LikedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.BookLikes)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookLikes__BookI__48CFD27E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BB735F03B");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0701DB157").IsUnique();

            entity.Property(e => e.CategoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF87C7603B223");

            entity.Property(e => e.RatingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.RatingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Ratings__BookId__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
