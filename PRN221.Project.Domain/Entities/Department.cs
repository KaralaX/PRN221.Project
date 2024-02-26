using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class Department
{
    public Department()
    {
        Services = new HashSet<Service>();
    }

    [Key]
    public Guid Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = null!;
    [StringLength(100)]
    public string Description { get; set; } = null!;
    public bool Status { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Service> Services { get; set; }
}