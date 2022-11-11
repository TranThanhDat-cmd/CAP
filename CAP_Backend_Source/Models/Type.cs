using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Type
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Test> Tests { get; } = new List<Test>();
}
