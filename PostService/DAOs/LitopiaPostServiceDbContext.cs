using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PostService.Models;

public partial class LitopiaPostServiceDbContext : DbContext
{
    public LitopiaPostServiceDbContext()
    {
    }

    public LitopiaPostServiceDbContext(DbContextOptions<LitopiaPostServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Hashtag> Hashtags { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostTaggedFriend> PostTaggedFriends { get; set; }

    public virtual DbSet<Reaction> Reactions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=THUC;Initial Catalog=Litopia_PostServiceDB;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCAAF45ADF1");

            entity.Property(e => e.CommentId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__PostId__4E88ABD4");

            entity.HasMany(d => d.Comments).WithMany(p => p.Replies)
                .UsingEntity<Dictionary<string, object>>(
                    "CommentReply",
                    r => r.HasOne<Comment>().WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CommentRe__Comme__534D60F1"),
                    l => l.HasOne<Comment>().WithMany()
                        .HasForeignKey("ReplyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CommentRe__Reply__5441852A"),
                    j =>
                    {
                        j.HasKey("CommentId", "ReplyId").HasName("PK__CommentR__4F913BAAE7011911");
                        j.ToTable("CommentReplies");
                    });

            entity.HasMany(d => d.Replies).WithMany(p => p.Comments)
                .UsingEntity<Dictionary<string, object>>(
                    "CommentReply",
                    r => r.HasOne<Comment>().WithMany()
                        .HasForeignKey("ReplyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CommentRe__Reply__5441852A"),
                    l => l.HasOne<Comment>().WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CommentRe__Comme__534D60F1"),
                    j =>
                    {
                        j.HasKey("CommentId", "ReplyId").HasName("PK__CommentR__4F913BAAE7011911");
                        j.ToTable("CommentReplies");
                    });
        });

        modelBuilder.Entity<Hashtag>(entity =>
        {
            entity.HasKey(e => e.HashtagId).HasName("PK__Hashtags__BEFA912AA6A8F291");

            entity.HasIndex(e => e.Name, "UQ__Hashtags__737584F60F2411A7").IsUnique();

            entity.Property(e => e.HashtagId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UsageCount).HasDefaultValue(0);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Images__7516F70CC54FB25D");

            entity.Property(e => e.ImageId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Url).HasMaxLength(255);

            entity.HasOne(d => d.Post).WithMany(p => p.Images)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Images__PostId__4AB81AF0");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA126018E853E23B");

            entity.Property(e => e.PostId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasMany(d => d.Hashtags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostHashtag",
                    r => r.HasOne<Hashtag>().WithMany()
                        .HasForeignKey("HashtagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PostHasht__Hasht__440B1D61"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PostHasht__PostI__4316F928"),
                    j =>
                    {
                        j.HasKey("PostId", "HashtagId").HasName("PK__PostHash__11FDC90A5905D783");
                        j.ToTable("PostHashtags");
                    });
        });

        modelBuilder.Entity<PostTaggedFriend>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.UserId }).HasName("PK__PostTagg__7B6AECDC20A0EACC");

            entity.HasOne(d => d.Post).WithMany(p => p.PostTaggedFriends)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostTagge__PostI__46E78A0C");
        });

        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.HasKey(e => e.ReactionId).HasName("PK__Reaction__46DDF9B4C4B3E5FF");

            entity.Property(e => e.ReactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reaction1)
                .HasMaxLength(20)
                .HasColumnName("Reaction");

            entity.HasOne(d => d.Post).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reactions__PostI__5812160E");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD4805D3730A0E");

            entity.Property(e => e.ReportId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.ReviewedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Post).WithMany(p => p.Reports)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__PostId__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
