namespace PRN221.Project.Domain.Entities;

public class Doctor
{
    public Doctor()
    {
        Appointments = new HashSet<Appointment>();
        Services = new HashSet<Service>();
    }

    public Guid Id { get; set; }
    public string? Address { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Dob { get; set; }
    public string? Gender { get; set; }
    public string UserId { get; set; } = null!;
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Service> Services { get; set; }
}