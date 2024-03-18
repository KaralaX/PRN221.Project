namespace PRN221.Project.Domain.Entities;

public class Department
{
    public Department()
    {
        Services = new HashSet<Service>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Status { get; set; }

    public virtual ICollection<Service> Services { get; set; }
}