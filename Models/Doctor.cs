using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? PhoneNumber { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Workhour> Workhours { get; set; } = new List<Workhour>();

    public virtual ICollection<Office> Offices { get; set; } = new List<Office>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();
}
