using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public int AccountIdRespondent { get; set; }

    public int QuestionContentId { get; set; }

    public virtual Question? Question { get; set; }
    public virtual Account? Account { get; set; }
    public virtual QuestionContent? QuestionContent { get; set; }
}
