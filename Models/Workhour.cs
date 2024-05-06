using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Workhour
{
    public int Id { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public int ReceptionistId { get; set; }

    public int ReceptionistUserId { get; set; }

    public int DoctorId { get; set; }

    public int DoctorUserId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Receptionist Receptionist { get; set; } = null!;
}
