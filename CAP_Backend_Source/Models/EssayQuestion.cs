using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class EssayQuestion
{
    public int EquestionId { get; set; }

    public int TestsId { get; set; }

    public string EquestionTitle { get; set; } = null!;

    public virtual ICollection<EssayAnswer> EssayAnswers { get; } = new List<EssayAnswer>();

    public virtual Test Tests { get; set; } = null!;
}
