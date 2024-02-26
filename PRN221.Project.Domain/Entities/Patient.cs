using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            ServiceReviews = new HashSet<ServiceReview>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(450)]
        public string UserId { get; set; } = null!;

        [InverseProperty("Patient")]
        public virtual PatientMedicalRecord? PatientMedicalRecord { get; set; }
        [InverseProperty("Id1")]
        public virtual PersonalInformation? PersonalInformation { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<ServiceReview> ServiceReviews { get; set; }
    }
}
