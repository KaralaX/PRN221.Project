namespace PRN221.Project.Domain.Entities;

public partial class Schedule
{
    public Schedule()
    {
        Appointments = new HashSet<Appointment>();
    }

    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public int NumAppointment { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
    public virtual Service Service { get; set; } = null!;
    public virtual ICollection<Appointment> Appointments { get; set; }
}