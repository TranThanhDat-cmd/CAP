using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Program
{
    public int ProgramId { get; set; }

    public int AccountIdCreator { get; set; }

    public int FacultyId { get; set; }

    public int CategoryId { get; set; }

    public string ProgramName { get; set; } = null!;

    public string? Image { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublish { get; set; }

    public int? Coin { get; set; }

    public int? PositionId { get; set; }

    public virtual Account AccountIdCreatorNavigation { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ContentProgram> ContentPrograms { get; } = new List<ContentProgram>();

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual Position? Position { get; set; }
}
