using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class MedicalBill
{
    [Key]
    public Guid AppointmentId { get; set; }
    [Column(TypeName = "money")]
    public decimal? Price { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("MedicalBill")]
    public virtual Appointment Appointment { get; set; } = null!;
}