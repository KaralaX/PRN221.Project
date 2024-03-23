using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN221.Project.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public string Status { get; set; } = null!;
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
    public virtual Patient Patient { get; set; } = null!;
    public virtual Service Service { get; set; } = null!;
    public virtual MedicalBill? MedicalBill { get; set; }
    public virtual ServiceReview? ServiceReview { get; set; }
}