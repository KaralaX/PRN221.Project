using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

[Table("ServiceReview")]
public partial class ServiceReview
{
    [Key]
    public Guid Id { get; set; }
    public int Rating { get; set; }
    [StringLength(500)]
    public string Comment { get; set; } = null!;
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid PatientId { get; set; }
    public Guid AppointmentId { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("ServiceReviews")]
    public virtual Appointment Appointment { get; set; } = null!;
    [ForeignKey("DoctorId")]
    [InverseProperty("ServiceReviews")]
    public virtual Doctor Doctor { get; set; } = null!;
    [ForeignKey("PatientId")]
    [InverseProperty("ServiceReviews")]
    public virtual Patient Patient { get; set; } = null!;
    [ForeignKey("ServiceId")]
    [InverseProperty("ServiceReviews")]
    public virtual Service Service { get; set; } = null!;
}