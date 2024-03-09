namespace PRN221.Project.Domain.Entities;

public partial class Patient
{
    public Patient()
    {
        Appointments = new HashSet<Appointment>();
        ServiceReviews = new HashSet<ServiceReview>();
    }

    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;

    public virtual PatientMedicalRecord? PatientMedicalRecord { get; set; }
    public virtual PersonalInformation? PersonalInformation { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<ServiceReview> ServiceReviews { get; set; }
}