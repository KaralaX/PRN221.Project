using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221.Project.Domain.Entities;

public partial class Schedule
{
    public Schedule()
    {
        Appointments = new HashSet<Appointment>();
    }

    [Key]
    public Guid Id { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime DateTime { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public int NumAppointment { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Schedules")]
    public virtual Doctor Doctor { get; set; } = null!;
    [ForeignKey("ServiceId")]
    [InverseProperty("Schedules")]
    public virtual Service Service { get; set; } = null!;
    [InverseProperty("Schedule")]
    public virtual ICollection<Appointment> Appointments { get; set; }
}