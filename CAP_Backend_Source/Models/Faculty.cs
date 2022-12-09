using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string? FacultyName { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } 

    public virtual ICollection<Program> Programs { get; set; } 
}
