using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class Appointment
{
    public Appointment()
    {
        ServiceReviews = new HashSet<ServiceReview>();
    }

    [Key]
    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    [StringLength(50)]
    public string Status { get; set; } = null!;
    public Guid PatientId { get; set; }
    
    [ForeignKey("PatientId")]
    [InverseProperty("Appointments")]
    public virtual Patient Patient { get; set; } = null!;
    [ForeignKey("ScheduleId")]
    [InverseProperty("Appointments")]
    public virtual Schedule Schedule { get; set; } = null!;
    [InverseProperty("Appointment")]
    public virtual AppointmentResult? AppointmentResult { get; set; }
    [InverseProperty("Appointment")]
    public virtual MedicalBill? MedicalBill { get; set; }
    [InverseProperty("Appointment")]
    public virtual ICollection<ServiceReview> ServiceReviews { get; set; }
}