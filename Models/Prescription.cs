using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Prescription
{
    public int Id { get; set; }

    public DateTime? DateOfPrescription { get; set; }

    public DateTime? RealisationDate { get; set; }

    public string? Code { get; set; }

    public int PatientId { get; set; }

    public string? Pdf { get; set; }

    public int? DoctorId { get; set; }

    public int? DoctorUserId { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
