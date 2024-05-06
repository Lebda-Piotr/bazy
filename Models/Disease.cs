using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Disease
{
    public string Name { get; set; } = null!;

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public sbyte? IsEnded { get; set; }

    public string? Comments { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
