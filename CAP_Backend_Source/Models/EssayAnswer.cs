using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class EssayAnswer
{
    public int EanswerId { get; set; }

    public int? EquestionId { get; set; }

    public int AccountIdRespondent { get; set; }

    public string EanswerContent { get; set; } = null!;

    public virtual Account AccountIdRespondentNavigation { get; set; } = null!;

    public virtual EssayQuestion? Equestion { get; set; }
}
