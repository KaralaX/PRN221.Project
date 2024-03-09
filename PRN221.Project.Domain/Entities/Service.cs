namespace PRN221.Project.Domain.Entities;

public partial class Service
{
    public Service()
    {
        Schedules = new HashSet<Schedule>();
        ServiceReviews = new HashSet<ServiceReview>();
        Doctors = new HashSet<Doctor>();
    }

    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Status { get; set; }
    public decimal Price { get; set; }
    public long? Duration { get; set; }

    public virtual Department Department { get; set; } = null!;
    public virtual ICollection<Schedule> Schedules { get; set; }
    public virtual ICollection<ServiceReview> ServiceReviews { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; }
}