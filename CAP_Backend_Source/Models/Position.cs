using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string? PositionName { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<ProgramPosition> ProgramPositions { get; set; } = new List<ProgramPosition>();
}
