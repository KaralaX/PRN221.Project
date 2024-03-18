namespace PRN221.Project.Domain.Entities;

public class PersonalInformation
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Dob { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }

    public virtual Patient Id1 { get; set; } = null!;
    public virtual Staff Id2 { get; set; } = null!;
    public virtual Doctor IdNavigation { get; set; } = null!;
}