using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Data;

public partial class KoiShowManagementSystemContext : DbContext
{
    public KoiShowManagementSystemContext()
    {
    }

    public KoiShowManagementSystemContext(DbContextOptions<KoiShowManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<JudgeScore> JudgeScores { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<KoiRegistration> KoiRegistrations { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=KoiShowManagementSystem;UID=sa;PWD=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0752DCB00A");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC07EC99EA4E");

            entity.ToTable("Competition");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Competiti__Categ__2B3F6F97");
        });

        modelBuilder.Entity<JudgeScore>(entity =>
        {
            entity.HasKey(e => e.ScoreId).HasName("PK__JudgeSco__7DD229D1CCBBF767");

            entity.ToTable("JudgeScore");

            entity.Property(e => e.BodyScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ColorScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.PatternScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TotalScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Competition).WithMany(p => p.JudgeScores)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__JudgeScor__Compe__34C8D9D1");

            entity.HasOne(d => d.Koi).WithMany(p => p.JudgeScores)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__JudgeScor__Koi_I__33D4B598");

            entity.HasOne(d => d.User).WithMany(p => p.JudgeScores)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__JudgeScor__User___32E0915F");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiFish__3214EC07F31FBB15");

            entity.ToTable("KoiFish");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Variety).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__KoiFish__User_Id__286302EC");
        });

        modelBuilder.Entity<KoiRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiRegis__3214EC0712E1B527");

            entity.ToTable("KoiRegistration");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.KoiRegistrations)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__KoiRegist__Categ__300424B4");

            entity.HasOne(d => d.Competition).WithMany(p => p.KoiRegistrations)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__KoiRegist__Compe__2F10007B");

            entity.HasOne(d => d.Koi).WithMany(p => p.KoiRegistrations)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__KoiRegist__Koi_I__2E1BDC42");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Result__3214EC0736D8B972");

            entity.ToTable("Result");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.Rank).HasMaxLength(50);
            entity.Property(e => e.TotalScore).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Results)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Result__Category__398D8EEE");

            entity.HasOne(d => d.Competition).WithMany(p => p.Results)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Result__Competit__38996AB5");

            entity.HasOne(d => d.Koi).WithMany(p => p.Results)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Result__Koi_Id__37A5467C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC074D613C26");

            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
