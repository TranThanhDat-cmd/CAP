using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class ProgramPosition
{
    public int ProgramId { get; set; }

    public int PositionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Position Position { get; set; } = null!;

    public virtual Program Program { get; set; } = null!;
}
