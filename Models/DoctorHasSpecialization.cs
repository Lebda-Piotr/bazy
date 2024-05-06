using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class DoctorHasSpecialization
{
    public int DoctorId { get; set; }

    public int DoctorUserId { get; set; }

    public int SpecializationId { get; set; }

    public virtual Specialization Specialization { get; set; } = null!;
}
