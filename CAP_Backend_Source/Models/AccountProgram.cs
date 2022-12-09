using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class AccountProgram
{
    public int ProgramId { get; set; }

    public int AccountId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Program Program { get; set; } = null!;
}
