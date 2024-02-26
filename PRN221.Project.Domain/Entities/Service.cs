using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class Service
{
    public Service()
    {
        Schedules = new HashSet<Schedule>();
        ServiceReviews = new HashSet<ServiceReview>();
        Doctors = new HashSet<Doctor>();
    }

    [Key]
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = null!;
    [StringLength(100)]
    public string Description { get; set; } = null!;
    public bool Status { get; set; }
    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Services")]
    public virtual Department Department { get; set; } = null!;
    [InverseProperty("Service")]
    public virtual ICollection<Schedule> Schedules { get; set; }
    [InverseProperty("Service")]
    public virtual ICollection<ServiceReview> ServiceReviews { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("Services")]
    public virtual ICollection<Doctor> Doctors { get; set; }
}