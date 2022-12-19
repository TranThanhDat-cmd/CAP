using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAP_Backend_Source.Models;

public partial class Program
{
    public int ProgramId { get; set; }

    public int? AccountIdCreator { get; set; }

    public int? FacultyId { get; set; }

    public int? CategoryId { get; set; }

    public string ProgramName { get; set; } = null!;

    public string? Image { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublish { get; set; }

    public int? Coin { get; set; }
    public int? Time { get; set; }

    public int? AcademicYearId { get; set; }

    public int? Semester { get; set; }

    public string? Descriptions { get; set; }
    public string? Lecturers { get; set; }

    public string? Status { get; set; }

    public DateTime? RegistrationStartDate { get; set; }

    public DateTime? RegistrationEndDate { get; set; }

    public virtual AcademicYear? AcademicYear { get; set; }

    public virtual Account? AccountIdCreatorNavigation { get; set; }

    public virtual ICollection<AccountProgram> AccountPrograms { get; set; } = new List<AccountProgram>();

    [NotMapped]
    public int? LearnerCount { get => AccountPrograms.Count; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ContentProgram> ContentPrograms { get; set; } = new List<ContentProgram>();

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<ProgramPosition> ProgramPositions { get; set; } = new List<ProgramPosition>();

    public virtual ICollection<Reviewer> Reviewers { get; } = new List<Reviewer>();

    public virtual ICollection<ReviewerProgram> ReviewsProgram { get; } = new List<ReviewerProgram>();
}
