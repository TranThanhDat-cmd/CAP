using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class QuestionType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
