namespace PRN221.Project.Domain.Entities
{
    public partial class AppointmentResult
    {
        public Guid AppointmentId { get; set; }
        public string? TreatmentPlan { get; set; }
        public string? TestResult { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;
    }
}
