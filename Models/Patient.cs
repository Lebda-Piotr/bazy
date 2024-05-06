using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Patient
{
    public string? Pesel { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public DateTime? NextVisit { get; set; }

    public DateTime? LastVisit { get; set; }

    public string Sex { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string? SecondName { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Referral> Referrals { get; set; } = new List<Referral>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Disease> Diseases { get; set; } = new List<Disease>();

<<<<<<< HEAD
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
	public override string ToString() {
		return Name+" "+Surname;
=======
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

	public override string ToString() {
        return Name + " " + Surname;
>>>>>>> fa686bf06a96693db22da0d341eeb7829d56cde2
	}
}
