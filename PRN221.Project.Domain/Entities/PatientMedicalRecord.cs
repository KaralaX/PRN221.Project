using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities
{
    public partial class PatientMedicalRecord
    {
        [Key]
        public Guid PatientId { get; set; }
        [Column(TypeName = "text")]
        public string? Allergies { get; set; }
        [Column(TypeName = "text")]
        public string? Medications { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("PatientMedicalRecord")]
        public virtual Patient Patient { get; set; } = null!;
    }
}
