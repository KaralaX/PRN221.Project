namespace PRN221.Project.Domain.Entities;

public class PatientMedicalRecord
{
    public Guid PatientId { get; set; }
    public string? FileName { get; set; }
    public byte[]? FileContent { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? UpdatedByStaff { get; set; }

    public virtual Patient Patient { get; set; } = null!;
    public virtual Staff? UpdatedByStaffNavigation { get; set; }
}