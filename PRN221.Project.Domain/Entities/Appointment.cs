namespace PRN221.Project.Domain.Entities;

public partial class Appointment
{
    public Appointment()
    {
        ServiceReviews = new HashSet<ServiceReview>();
    }

    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string Status { get; set; } = null!;
    public Guid PatientId { get; set; }

    public virtual Patient Patient { get; set; } = null!;
    public virtual Schedule Schedule { get; set; } = null!;
    public virtual AppointmentResult? AppointmentResult { get; set; }
    public virtual MedicalBill? MedicalBill { get; set; }
    public virtual ICollection<ServiceReview> ServiceReviews { get; set; }
}