namespace PRN221.Project.Domain.Entities;

public class Doctor
{
    public Doctor()
    {
        Appointments = new HashSet<Appointment>();
        Services = new HashSet<Service>();
    }

    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;

    public virtual PersonalInformation? PersonalInformation { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Service> Services { get; set; }
}