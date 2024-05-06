using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class User
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Hash { get; set; }

    public bool FirstLogin { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Administrator> Administrators { get; set; } = new List<Administrator>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Receptionist> Receptionists { get; set; } = new List<Receptionist>();
}
