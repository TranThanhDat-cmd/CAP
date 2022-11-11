using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int RoleId { get; set; }

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? ExtendnalId { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public virtual ICollection<EssayAnswer> EssayAnswers { get; } = new List<EssayAnswer>();

    public virtual ICollection<Learner> LearnerAccountIdApproverNavigations { get; } = new List<Learner>();

    public virtual ICollection<Learner> LearnerAccountIdLearnerNavigations { get; } = new List<Learner>();

    public virtual ICollection<MultipleChoiceAnswer> MultipleChoiceAnswers { get; } = new List<MultipleChoiceAnswer>();

    public virtual ICollection<Program> Programs { get; } = new List<Program>();

    public virtual Role Role { get; set; } = null!;
}
