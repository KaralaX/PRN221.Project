using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class PersonalInformation
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(50)]
    public string? FirstName { get; set; }
    [StringLength(50)]
    public string? LastName { get; set; }
    public DateTime? Dob { get; set; }
    [StringLength(20)]
    public string? Gender { get; set; }
    [StringLength(100)]
    public string? Address { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("PersonalInformation")]
    public virtual Patient Id1 { get; set; } = null!;
    [ForeignKey("Id")]
    [InverseProperty("PersonalInformation")]
    public virtual Staff Id2 { get; set; } = null!;
    [ForeignKey("Id")]
    [InverseProperty("PersonalInformation")]
    public virtual Doctor IdNavigation { get; set; } = null!;
}