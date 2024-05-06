using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class DoctorPatient
{
    public int DoctorId { get; set; }

    public int DoctorUserId { get; set; }

    public int PatientId { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
