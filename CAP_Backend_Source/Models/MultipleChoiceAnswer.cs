using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class MultipleChoiceAnswer
{
    public int McanswerId { get; set; }

    public int? McquestionId { get; set; }

    public int AccountIdRespondent { get; set; }

    public string McanswerContent { get; set; } = null!;

    public virtual Account AccountIdRespondentNavigation { get; set; } = null!;

    public virtual MultipleChoiceQuestion? Mcquestion { get; set; }
}
