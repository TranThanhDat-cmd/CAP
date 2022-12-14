using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int? RoleId { get; set; }

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? PositionId { get; set; }

    public int? FacultyId { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<AccountProgram> AccountPrograms { get; } = new List<AccountProgram>();

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<Learner> LearnerAccountIdApproverNavigations { get; } = new List<Learner>();

    public virtual ICollection<Learner> LearnerAccountIdLearnerNavigations { get; } = new List<Learner>();

    public virtual Position? Position { get; set; }

    public virtual ICollection<Program> Programs { get; } = new List<Program>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Reviewer> Reviewers { get; } = new List<Reviewer>();

    public virtual ICollection<ReviewerProgram> ReviewsProgram { get; } = new List<ReviewerProgram>();
    public virtual ICollection<Answer> Answer { get; } = new List<Answer>();
    public virtual ICollection<ResultTest> ResultTest { get; } = new List<ResultTest>();
}
