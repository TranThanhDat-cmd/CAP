﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAP_Backend_Source.Models;

public partial class Program
{
    public int ProgramId { get; set; }

    [NotMapped]
    public List<Position> Position { get; set; }

    public int? AccountIdCreator { get; set; }

    public int? FacultyId { get; set; }

    public int? CategoryId { get; set; }

    public string ProgramName { get; set; } = null!;

    public string? Image { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublish { get; set; } = false;

    public int? Coin { get; set; }

    public string Positions { get; set; } = null!;

    public int? AcademicYearId { get; set; }

    public int? Semester { get; set; }

    public string? Descriptions { get; set; }

    public string? Status { get; set; }

    public virtual AcademicYear? AcademicYear { get; set; }

    public virtual Account? AccountIdCreatorNavigation { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ContentProgram> ContentPrograms { get; } = new List<ContentProgram>();

    public virtual Faculty? Faculty { get; set; }
}
