namespace PRN221.Project.Domain.Entities;

public class Patient
{
    public Patient()
    {
        Appointments = new HashSet<Appointment>();
    }

    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;

    public virtual PatientMedicalRecord? PatientMedicalRecord { get; set; }
    public virtual PersonalInformation? PersonalInformation { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
}