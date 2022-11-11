using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Program> Programs { get; } = new List<Program>();
}
