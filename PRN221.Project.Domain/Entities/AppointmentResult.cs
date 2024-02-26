using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class AppointmentResult
{
    [Key]
    public Guid AppointmentId { get; set; }
    [Column(TypeName = "text")]
    public string? TreatmentPlan { get; set; }
    [Column(TypeName = "text")]
    public string? TestResult { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("AppointmentResult")]
    public virtual Appointment Appointment { get; set; } = null!;
}