using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Medicine
{
    public string? Name { get; set; }

    public string? Dose { get; set; }

    public int Id { get; set; }

    public int? Amount { get; set; }

    public string? Comments { get; set; }

    public float? Fraction { get; set; }

    public virtual ICollection<Disease> Dieseases { get; set; } = new List<Disease>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
