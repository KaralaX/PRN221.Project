namespace PRN221.Project.Domain.Entities;

public class Staff
{
    public Staff()
    {
        PatientMedicalRecords = new HashSet<PatientMedicalRecord>();
    }

    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;

    public virtual PersonalInformation? PersonalInformation { get; set; }
    public virtual ICollection<PatientMedicalRecord> PatientMedicalRecords { get; set; }
}