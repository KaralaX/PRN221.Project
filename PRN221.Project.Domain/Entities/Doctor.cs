using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class Doctor
{
    public Doctor()
    {
        Schedules = new HashSet<Schedule>();
        ServiceReviews = new HashSet<ServiceReview>();
        Services = new HashSet<Service>();
    }

    [Key]
    public Guid Id { get; set; }
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [InverseProperty("IdNavigation")]
    public virtual PersonalInformation? PersonalInformation { get; set; }
    [InverseProperty("Doctor")]
    public virtual ICollection<Schedule> Schedules { get; set; }
    [InverseProperty("Doctor")]
    public virtual ICollection<ServiceReview> ServiceReviews { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Doctors")]
    public virtual ICollection<Service> Services { get; set; }
}