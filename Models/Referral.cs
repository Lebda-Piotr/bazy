using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Referral
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public int PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? DoctorUserId { get; set; }

    public string? Information { get; set; }

    public string? MedicalEntity { get; set; }

    public string? Pdf { get; set; }

    public DateTime? Date { get; set; }

    public string? Disease { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
