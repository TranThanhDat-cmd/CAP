using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class AcademicYear
{
    public int Id { get; set; }

    public string? Year { get; set; }

    public virtual ICollection<Program> Programs { get; } = new List<Program>();
}
