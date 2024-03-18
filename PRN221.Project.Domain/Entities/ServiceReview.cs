namespace PRN221.Project.Domain.Entities;

public class ServiceReview
{
    public Guid AppointmentId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}