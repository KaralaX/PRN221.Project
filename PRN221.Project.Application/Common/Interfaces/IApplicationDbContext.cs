using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Appointment> Appointments { get; }
    DbSet<Department> Departments { get; }
    DbSet<Doctor> Doctors { get; }
    DbSet<MedicalBill> MedicalBills { get; }
    DbSet<Patient> Patients { get; }
    DbSet<Staff> Staffs { get; }
    DbSet<PatientMedicalRecord> PatientMedicalRecords { get; }
    DbSet<Service> Services { get; }
    DbSet<ServiceReview> ServiceReviews { get; }
    Task<int> SaveChangesAsync(CancellationToken token = default);
}