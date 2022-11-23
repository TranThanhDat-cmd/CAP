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


    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ContentProgram> ContentPrograms { get; set; }

    public virtual DbSet<EssayAnswer> EssayAnswers { get; set; }

    public virtual DbSet<EssayQuestion> EssayQuestions { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Learner> Learners { get; set; }

    public virtual DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }

    public virtual DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=tuleap.vanlanguni.edu.vn,18082;Initial Catalog=CP25Team02;User ID=CP25Team02;Password=CP25Team02;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__3214EC0795771D95");

            entity.ToTable("Account");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_ROLE");
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

        modelBuilder.Entity<EssayAnswer>(entity =>
        {
            entity.HasKey(e => e.EanswerId).HasName("PK__EssayAns__9F6FAEE82A910F29");

            entity.Property(e => e.EanswerId).HasColumnName("EAnswerId");
            entity.Property(e => e.EanswerContent).HasColumnName("EAnswerContent");
            entity.Property(e => e.EquestionId).HasColumnName("EQuestionId");



            entity.HasOne(d => d.Equestion).WithMany(p => p.EssayAnswers)
                .HasForeignKey(d => d.EquestionId)
                .HasConstraintName("FK_EssayAnswers_EssayQuestions");
        });

        modelBuilder.Entity<EssayQuestion>(entity =>
        {
            entity.HasKey(e => e.EquestionId).HasName("PK__EssayQue__7BE2A80C92D22CF3");

            entity.Property(e => e.EquestionId).HasColumnName("EQuestionId");
            entity.Property(e => e.EquestionTitle).HasColumnName("EQuestionTitle");

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

        modelBuilder.Entity<MultipleChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.McanswerId).HasName("PK__Multiple__6901F7AABF59D65D");

            entity.Property(e => e.McanswerId).HasColumnName("MCAnswerId");
            entity.Property(e => e.McanswerContent).HasColumnName("MCAnswerContent");
            entity.Property(e => e.McquestionId).HasColumnName("MCQuestionId");


            entity.HasOne(d => d.Mcquestion).WithMany(p => p.MultipleChoiceAnswers)
                .HasForeignKey(d => d.McquestionId)
                .HasConstraintName("FK_MultipleChoiceAnswers_MultipleChoiceQuestions");
        });

        modelBuilder.Entity<MultipleChoiceQuestion>(entity =>
        {
            entity.HasKey(e => e.McquestionId).HasName("PK__Multiple__61949E9788E53D29");

            entity.Property(e => e.McquestionId).HasColumnName("MCQuestionId");
            entity.Property(e => e.McquestionTitle).HasColumnName("MCQuestionTitle");

        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PK__Program__7525605855EAD961");

            entity.ToTable("Program");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.AccountIdCreatorNavigation).WithMany(p => p.Programs)
                .HasForeignKey(d => d.AccountIdCreator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.Programs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Category");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Programs)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Faculty");
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

            entity.HasOne(d => d.Program).WithMany(p => p.Tests)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_Program");

            entity.HasOne(d => d.Type).WithMany(p => p.Tests)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_Type");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Type__516F03B5233EFFD8");

            entity.ToTable("Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
