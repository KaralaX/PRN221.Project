namespace PRN221.Project.Domain.Entities;

public class Staff
{
    public Staff()
    {
        PatientMedicalRecords = new HashSet<PatientMedicalRecord>();
    }

    public Guid Id { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Dob { get; set; }
    public string UserId { get; set; } = null!;
    public virtual ICollection<PatientMedicalRecord> PatientMedicalRecords { get; set; }
}