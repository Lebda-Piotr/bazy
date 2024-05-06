using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class DoctorHasOffice
{
    public int DoctorId { get; set; }

    public int DoctorUserId { get; set; }

    public int OfficeId { get; set; }

    public virtual Office Office { get; set; } = null!;
}
