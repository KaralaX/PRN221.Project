using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class Staff
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [InverseProperty("Id2")]
    public virtual PersonalInformation? PersonalInformation { get; set; }
}