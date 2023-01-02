using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Learner
{
    public int LearnerId { get; set; }

    public int AccountIdLearner { get; set; }

    public int ProgramId { get; set; }

    public bool IsRegister { get; set; }

    public int? AccountIdApprover { get; set; }

    public string? Status { get; set; }

    public int? Score { get; set; }
    public string? ReasonRefusal { get; set; }
    public string? RegisterStatus { get; set; }
    public string? Comment { get; set; }

    public virtual Account AccountIdApproverNavigation { get; set; } = null!;

    public virtual Account AccountIdLearnerNavigation { get; set; } = null!;
}
