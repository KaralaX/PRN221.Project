﻿namespace PRN221.Project.Domain.Entities;

public class MedicalBill
{
    public Guid AppointmentId { get; set; }
    public decimal? Price { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}