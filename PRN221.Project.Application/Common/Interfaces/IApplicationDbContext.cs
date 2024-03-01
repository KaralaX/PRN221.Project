using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Appointment> Appointments { get; }
    DbSet<Schedule> Schedules { get; }
    DbSet<Doctor> Doctors { get; }
    DbSet<Patient> Patients { get; }
    DbSet<Staff> Staffs { get; }
    DbSet<PersonalInformation> PersonalInformations { get; }
    Task<int> SaveChangesAsync(CancellationToken token);
}