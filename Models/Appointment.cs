using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public string? Date { get; set; }

    public string? Goal { get; set; }

    public int NotificationId { get; set; }

    public int PatientId { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Receptionist> Receptionists { get; set; } = new List<Receptionist>();
}
