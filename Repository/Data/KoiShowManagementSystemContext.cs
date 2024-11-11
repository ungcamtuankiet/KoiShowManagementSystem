using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

    public virtual DbSet<Matchup> Matchups { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<ResultDetail> ResultDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC074FA6B999");

            entity.ToTable("Category");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC0761B5A4F7");

            entity.ToTable("Competition");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Competiti__Categ__2B3F6F97");
        });

        modelBuilder.Entity<JudgeScore>(entity =>
        {
            entity.HasKey(e => e.ScoreId).HasName("PK__JudgeSco__7DD229D112F378D5");

            entity.ToTable("JudgeScore");

            entity.Property(e => e.BodyScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ColorScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.PatternScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
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
            entity.HasKey(e => e.Id).HasName("PK__KoiFish__3214EC07D2C543C5");

            entity.ToTable("KoiFish");

            entity.Property(e => e.AvatarUrl).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Variety).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__KoiFish__User_Id__286302EC");
        });

        modelBuilder.Entity<KoiRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiRegis__3214EC07E0594F29");

            entity.ToTable("KoiRegistration");

            entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Competition).WithMany(p => p.KoiRegistrations)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__KoiRegist__Compe__5441852A");

            entity.HasOne(d => d.Koi).WithMany(p => p.KoiRegistrations)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__KoiRegist__Koi_I__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.KoiRegistrations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__KoiRegist__User___5535A963");
        });

        modelBuilder.Entity<Matchup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matchup__3214EC07ABA8FC8F");

            entity.ToTable("Matchup");

            entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.KoiId1).HasColumnName("Koi_Id1");
            entity.Property(e => e.KoiId2).HasColumnName("Koi_Id2");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Competition).WithMany(p => p.Matchups)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Matchup__Competi__60A75C0F");

            entity.HasOne(d => d.KoiId1Navigation).WithMany(p => p.MatchupKoiId1Navigations)
                .HasForeignKey(d => d.KoiId1)
                .HasConstraintName("FK__Matchup__Koi_Id1__619B8048");

            entity.HasOne(d => d.KoiId2Navigation).WithMany(p => p.MatchupKoiId2Navigations)
                .HasForeignKey(d => d.KoiId2)
                .HasConstraintName("FK__Matchup__Koi_Id2__628FA481");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Result__3214EC0742EB1A33");

            entity.ToTable("Result");

            entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.Rank).HasMaxLength(50);
            entity.Property(e => e.ResultKoi).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Competition).WithMany(p => p.Results)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Result__Competit__59063A47");

            entity.HasOne(d => d.Koi).WithMany(p => p.Results)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Result__Koi_Id__5812160E");
        });

        modelBuilder.Entity<ResultDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ResultDe__3214EC070E3051B5");

            entity.ToTable("ResultDetail");

            entity.Property(e => e.ResultId).HasColumnName("Result_Id");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.KoiWinNavigation).WithMany(p => p.ResultDetails)
                .HasForeignKey(d => d.KoiWin)
                .HasConstraintName("FK__ResultDet__KoiWi__5DCAEF64");

            entity.HasOne(d => d.Result).WithMany(p => p.ResultDetails)
                .HasForeignKey(d => d.ResultId)
                .HasConstraintName("FK__ResultDet__Resul__5CD6CB2B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC070C7BDA9F");

            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
