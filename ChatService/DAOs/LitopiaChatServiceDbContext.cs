using System;
using System.Collections.Generic;
using ChatService.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatService.DAOs;

public partial class LitopiaChatServiceDbContext : DbContext
{
    public LitopiaChatServiceDbContext()
    {
    }

    public LitopiaChatServiceDbContext(DbContextOptions<LitopiaChatServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupBook> GroupBooks { get; set; }

    public virtual DbSet<GroupBookNote> GroupBookNotes { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<GroupPendingMember> GroupPendingMembers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageMedium> MessageMedia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=THUC;Initial Catalog=Litopia_ChatServiceDB;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36AE220C056");

            entity.Property(e => e.GroupId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
        });

        modelBuilder.Entity<GroupBook>(entity =>
        {
            entity.HasKey(e => e.GroupBookId).HasName("PK__GroupBoo__E18A30175986C0B7");

            entity.Property(e => e.GroupBookId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CurrentChapter).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupBooks)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBook__Group__52593CB8");
        });

        modelBuilder.Entity<GroupBookNote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__GroupBoo__EACE355F792FDEE5");

            entity.Property(e => e.NoteId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).HasMaxLength(500);

            entity.HasOne(d => d.GroupBook).WithMany(p => p.GroupBookNotes)
                .HasForeignKey(d => d.GroupBookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupBook__Group__5535A963");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => e.GroupMemberId).HasName("PK__GroupMem__344812923DF21BDA");

            entity.Property(e => e.GroupMemberId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.JoinedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupMemb__Group__4BAC3F29");
        });

        modelBuilder.Entity<GroupPendingMember>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.UserId }).HasName("PK__GroupPen__C5E27FAEA95D4EBC");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupPendingMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupPend__Group__47DBAE45");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9CEA161FB4");

            entity.Property(e => e.MessageId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Type).HasMaxLength(20);
        });

        modelBuilder.Entity<MessageMedium>(entity =>
        {
            entity.HasKey(e => new { e.MessageId, e.MediaUrl }).HasName("PK__MessageM__49105955CF04B781");

            entity.Property(e => e.MediaUrl).HasMaxLength(255);

            entity.HasOne(d => d.Message).WithMany(p => p.MessageMedia)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MessageMe__Messa__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
