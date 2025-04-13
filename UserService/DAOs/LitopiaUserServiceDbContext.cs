using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.DAOs;

public partial class LitopiaUserServiceDbContext : DbContext
{
    public LitopiaUserServiceDbContext()
    {
    }

    public LitopiaUserServiceDbContext(DbContextOptions<LitopiaUserServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=THUC;Initial Catalog=Litopia_UserServiceDB;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__FriendRe__33A8517A72C5EE05");

            entity.Property(e => e.RequestId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Receiver).WithMany(p => p.FriendRequestReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FriendReq__Recei");

            entity.HasOne(d => d.Sender).WithMany(p => p.FriendRequestSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FriendReq__Sende");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A7A7B39D4");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160E447266E").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CB77B42D0");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4AC230E64").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341DEF487B").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.Bio).HasMaxLength(80);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailConfirmed).HasDefaultValue(false);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IdentificationNumber).HasMaxLength(50);
            entity.Property(e => e.Otp)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("OTP");
            entity.Property(e => e.OtpCreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("OTPCreatedAt");
            entity.Property(e => e.Password).HasMaxLength(64);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PhoneConfirmed).HasDefaultValue(false);
            entity.Property(e => e.ReportCount).HasDefaultValue(0);
            entity.Property(e => e.UpToAuthor).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
