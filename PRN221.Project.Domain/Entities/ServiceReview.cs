namespace PRN221.Project.Domain.Entities;

public partial class ServiceReview
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid PatientId { get; set; }
    public Guid AppointmentId { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
    public virtual Doctor Doctor { get; set; } = null!;
    public virtual Patient Patient { get; set; } = null!;
    public virtual Service Service { get; set; } = null!;
}