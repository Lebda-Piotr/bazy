using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Address
{
    public int Id { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? BuildingNumber { get; set; }

    public string? PostalCode { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
