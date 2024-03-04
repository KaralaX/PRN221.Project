namespace PRN221.Project.Domain.Entities;

public partial class PatientMedicalRecord
{
    public Guid PatientId { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}