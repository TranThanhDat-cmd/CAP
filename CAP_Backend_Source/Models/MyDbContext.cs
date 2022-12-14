using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicYear> AcademicYears { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountProgram> AccountPrograms { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ContentProgram> ContentPrograms { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Learner> Learners { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramPosition> ProgramPositions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionContent> QuestionContents { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Reviewer> Reviews { get; set; }

    public virtual DbSet<ReviewerProgram> ReviewsProgram { get; set; }

    public virtual DbSet<ResultTest> ResultTests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tuleap.vanlanguni.edu.vn,18082;Initial Catalog=CP25Team02;User ID=CP25Team02;Password=CP25Team02;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicYear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Academic__3214EC0771C023BA");

            entity.ToTable("AcademicYear");

            entity.Property(e => e.Year)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__3214EC0795771D95");

            entity.ToTable("Account");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PositionId).HasColumnName("PositionId\r\n");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_Account_Faculty");

            entity.HasOne(d => d.Position).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_Account_Position");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ROLE");
        });

        modelBuilder.Entity<AccountProgram>(entity =>
        {
            entity.HasKey(e => new { e.ProgramId, e.AccountId }).HasName("PK__AccountP__066CBA02DA8E8603");

            entity.ToTable("AccountProgram");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountPrograms)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AccProgramn_AccountId");

            entity.HasOne(d => d.Program).WithMany(p => p.AccountPrograms)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AccProgramn_ProgramId");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answer__D482500441027AB4");

            entity.ToTable("Answer");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Answer_Question");

            entity.HasOne(d => d.Account).WithMany(p => p.Answer)
                .HasForeignKey(d => d.AccountIdRespondent)
                .HasConstraintName("FK_Answer_Account");

            entity.HasOne(d => d.QuestionContent).WithMany(p => p.Answer)
                .HasForeignKey(d => d.QuestionContentId)
                .HasConstraintName("FK_Answer_QuestionContent");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BC38177CC");

            entity.ToTable("Category");
        });

        modelBuilder.Entity<ContentProgram>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__ContentP__2907A81EA494389F");

            entity.ToTable("ContentProgram");

            entity.HasOne(d => d.Program).WithMany(p => p.ContentPrograms)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentProgram_Program");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__Faculty__306F630EACD32E14");

            entity.ToTable("Faculty");
        });

        modelBuilder.Entity<Learner>(entity =>
        {
            entity.HasKey(e => e.LearnerId).HasName("PK__Learner__67ABFCDA39F21851");

            entity.ToTable("Learner");

            entity.HasOne(d => d.AccountIdApproverNavigation).WithMany(p => p.LearnerAccountIdApproverNavigations)
                .HasForeignKey(d => d.AccountIdApprover)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Learner_Account_Approver");

            entity.HasOne(d => d.AccountIdLearnerNavigation).WithMany(p => p.LearnerAccountIdLearnerNavigations)
                .HasForeignKey(d => d.AccountIdLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Learner_Account_Learner");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A79D114D8CA");

            entity.ToTable("Position");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PK__Program__7525605855EAD961");

            entity.ToTable("Program");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.RegistrationEndDate).HasColumnType("datetime");
            entity.Property(e => e.RegistrationStartDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.AcademicYear).WithMany(p => p.Programs)
                .HasForeignKey(d => d.AcademicYearId)
                .HasConstraintName("FK_Program_Year");

            entity.HasOne(d => d.AccountIdCreatorNavigation).WithMany(p => p.Programs)
                .HasForeignKey(d => d.AccountIdCreator)
                .HasConstraintName("FK_Program_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.Programs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Program_Category");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Programs)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_Program_Faculty");
        });

        modelBuilder.Entity<ProgramPosition>(entity =>
        {
            entity.HasKey(e => new { e.ProgramId, e.PositionId }).HasName("PK__ProgramP__F32ED9FF37B5C4F1");

            entity.ToTable("ProgramPosition");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Position).WithMany(p => p.ProgramPositions)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProgramPosition_PositionId");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramPositions)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProgramPosition_ProgramID");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC43EE1323");

            entity.ToTable("Question");

            entity.HasOne(d => d.Tests).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestsId)
                .HasConstraintName("FK_Question_Test");

            entity.HasOne(d => d.Type).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Type");
        });

        modelBuilder.Entity<QuestionContent>(entity =>
        {
            entity.HasKey(e => e.QuestionContentId).HasName("PK__Question__22E90A2CC67A1CDB");

            entity.ToTable("QuestionContent");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionContents)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QuestionContent_Question");
        });

        modelBuilder.Entity<QuestionType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Type__516F03B5233EFFD8");

            entity.ToTable("QuestionType");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AEE21B5FD");

            entity.ToTable("Role");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__8CC331607A0F55CF");

            entity.ToTable("Test");

            entity.HasOne(d => d.Content).WithMany(p => p.Tests)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_ContentProgram");
        });

        modelBuilder.Entity<Reviewer>(entity =>
        {
            entity.HasKey(e => e.ReviewerId).HasName("PK__Reviewer__1616CFDD2306341C");

            entity.ToTable("Reviewer");

            entity.HasOne(d => d.Account).WithMany(p => p.Reviewers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviewer_Account");

            entity.HasOne(d => d.Program).WithMany(p => p.Reviewers)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviewer_Program");
        });

        modelBuilder.Entity<ReviewerProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviewer__3214EC07694E4486");

            entity.ToTable("ReviewerProgram");

            entity.HasOne(d => d.Account).WithMany(p => p.ReviewsProgram)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewerProgram_Account");

            entity.HasOne(d => d.Program).WithMany(p => p.ReviewsProgram)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewerProgram_Program");
        });

        modelBuilder.Entity<ResultTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ResultTe__3214EC0730107C16");

            entity.ToTable("ResultTest");

            entity.HasOne(d => d.Account).WithMany(p => p.ResultTest)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_ResultTest_Account");

            entity.HasOne(d => d.Test).WithMany(p => p.ResultTest)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("[FK_ResultTest_Test]");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
