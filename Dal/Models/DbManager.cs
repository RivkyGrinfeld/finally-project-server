using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.Models;

public partial class DbManager : DbContext
{
    public DbManager()
    {
    }

    public DbManager(DbContextOptions<DbManager> options)
        : base(options)
    {
    }

    public virtual DbSet<AnswersTbl> AnswersTbls { get; set; }

    public virtual DbSet<ApplyTbl> ApplyTbls { get; set; }

    public virtual DbSet<BranchesTbl> BranchesTbls { get; set; }

    public virtual DbSet<CompaniesTbl> CompaniesTbls { get; set; }

    public virtual DbSet<CustomersTbl> CustomersTbls { get; set; }

    public virtual DbSet<ManagersTbl> ManagersTbls { get; set; }

    public virtual DbSet<PointsTestTbl> PointsTestTbls { get; set; }

    public virtual DbSet<PositionsTbl> PositionsTbls { get; set; }

    public virtual DbSet<PostsTbl> PostsTbls { get; set; }

    public virtual DbSet<PropertiesTbl> PropertiesTbls { get; set; }

    public virtual DbSet<QuestionsTbl> QuestionsTbls { get; set; }

    public virtual DbSet<RequestsTbl> RequestsTbls { get; set; }

    public virtual DbSet<StatusTbl> StatusTbls { get; set; }

    public virtual DbSet<TestsTbl> TestsTbls { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserVerificationToken> UserVerificationTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='D:\\c#\\Project - Copy\\Dal\\DataBase\\DBProject.mdf';Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswersTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answers___3214EC073BF746E7");

            entity.ToTable("Answers_tbl");

            entity.HasOne(d => d.Question).WithMany(p => p.AnswersTbls)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_tbl_ToTable");
        });

        modelBuilder.Entity<ApplyTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0781A18499");

            entity.ToTable("Apply_tbl");

            entity.Property(e => e.Confirmed).HasColumnName("confirmed");
            entity.Property(e => e.CustId)
                .HasMaxLength(9)
                .HasColumnName("custId");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.PostId).HasColumnName("postId");

            entity.HasOne(d => d.Cust).WithMany(p => p.ApplyTbls)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Apply_tbl_ToTable");

            entity.HasOne(d => d.Post).WithMany(p => p.ApplyTbls)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Apply_tbl_ToTable_1");
        });

        modelBuilder.Entity<BranchesTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Branches__3214EC078FD2D8BF");

            entity.ToTable("Branches_tbl");
        });

        modelBuilder.Entity<CompaniesTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC0707AA3EEF");

            entity.ToTable("Companies_tbl");

            entity.Property(e => e.UserId).HasDefaultValue(2);

            entity.HasOne(d => d.User).WithMany(p => p.CompaniesTbls)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_tbl_ToTable");
        });

        modelBuilder.Entity<CustomersTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC074A5C544C");

            entity.ToTable("Customers_tbl");

            entity.Property(e => e.Id).HasMaxLength(9);
            entity.Property(e => e.Address).HasMaxLength(30);
            entity.Property(e => e.BornDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.UserId).HasDefaultValue(2);

            entity.HasOne(d => d.Branch).WithMany(p => p.CustomersTbls)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_tbl_ToTable");

            entity.HasOne(d => d.User).WithMany(p => p.CustomersTbls)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_tbl_ToTable_1");
        });

        modelBuilder.Entity<ManagersTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Managers__3214EC076F62E3EA");

            entity.ToTable("Managers_tbl");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UserId).HasDefaultValue(2);

            entity.HasOne(d => d.User).WithMany(p => p.ManagersTbls)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Managers_tbl_ToTable");
        });

        modelBuilder.Entity<PointsTestTbl>(entity =>
        {
            entity.ToTable("PointsTest_tbl");

            entity.HasOne(d => d.Property).WithMany(p => p.PointsTestTbls)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PointsTest_tbl_ToTable_1");

            entity.HasOne(d => d.Test).WithMany(p => p.PointsTestTbls)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PointsTest_tbl_ToTable");
        });

        modelBuilder.Entity<PositionsTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC07B613316B");

            entity.ToTable("Positions_tbl");

            entity.HasOne(d => d.Branch).WithMany(p => p.PositionsTbls)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Poitions_tbl_ToTable");
        });

        modelBuilder.Entity<PostsTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts_tb__3214EC07ED5A1591");

            entity.ToTable("Posts_tbl");

            entity.Property(e => e.City).IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsConfirmed).HasColumnName("isConfirmed");
            entity.Property(e => e.MaxCadidated).HasDefaultValue(1);

            entity.HasOne(d => d.Company).WithMany(p => p.PostsTbls)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_tbl_ToTable");

            entity.HasOne(d => d.Position).WithMany(p => p.PostsTbls)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_tbl_ToTable_1");
        });

        modelBuilder.Entity<PropertiesTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07E25AA447");

            entity.ToTable("Properties_tbl");
        });

        modelBuilder.Entity<QuestionsTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07CDE16744");

            entity.ToTable("Questions_tbl");

            entity.HasOne(d => d.Property).WithMany(p => p.QuestionsTbls)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_tbl_ToTable");
        });

        modelBuilder.Entity<RequestsTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requests__3214EC07C3E27F3D");

            entity.ToTable("Requests_tbl");

            entity.HasOne(d => d.Post).WithMany(p => p.RequestsTbls)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_tbl_ToTable_1");

            entity.HasOne(d => d.Property).WithMany(p => p.RequestsTbls)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_tbl_ToTable");
        });

        modelBuilder.Entity<StatusTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status_t__3214EC07AF9EE372");

            entity.ToTable("Status_tbl");
        });

        modelBuilder.Entity<TestsTbl>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Tests_tb__8CC33160ED8B3800");

            entity.ToTable("Tests_tbl");

            entity.Property(e => e.CustId).HasMaxLength(9);

            entity.HasOne(d => d.Cust).WithMany(p => p.TestsTbls)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tests_tbl_ToTable");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07A57ED386");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_ToTable");
        });

        modelBuilder.Entity<UserVerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserVeri__3214EC0724AB5092");

            entity.Property(e => e.CreationTime).HasColumnType("datetime");
            entity.Property(e => e.ExpirationTime).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(500);
            entity.Property(e => e.UserId).HasMaxLength(9);

            entity.HasOne(d => d.User).WithMany(p => p.UserVerificationTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserVerif__UserI__09746778");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
