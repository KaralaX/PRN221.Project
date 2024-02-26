using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Infrastructure.Persistence
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AppointmentResult> AppointmentResults { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<MedicalBill> MedicalBills { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<PatientMedicalRecord> PatientMedicalRecords { get; set; } = null!;
        public virtual DbSet<PersonalInformation> PersonalInformations { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceReview> ServiceReviews { get; set; } = null!;
        public virtual DbSet<Staff> Staffs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointments_Patients");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointments_Schedules");
            });

            modelBuilder.Entity<AppointmentResult>(entity =>
            {
                entity.Property(e => e.AppointmentId).ValueGeneratedNever();

                entity.HasOne(d => d.Appointment)
                    .WithOne(p => p.AppointmentResult)
                    .HasForeignKey<AppointmentResult>(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentResults_Appointments");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasMany(d => d.Services)
                    .WithMany(p => p.Doctors)
                    .UsingEntity<Dictionary<string, object>>(
                        "DoctorService",
                        l => l.HasOne<Service>().WithMany().HasForeignKey("ServiceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DoctorServices_Services"),
                        r => r.HasOne<Doctor>().WithMany().HasForeignKey("DoctorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DoctorServices_Doctors"),
                        j =>
                        {
                            j.HasKey("DoctorId", "ServiceId");

                            j.ToTable("DoctorServices");
                        });
            });

            modelBuilder.Entity<MedicalBill>(entity =>
            {
                entity.Property(e => e.AppointmentId).ValueGeneratedNever();

                entity.HasOne(d => d.Appointment)
                    .WithOne(p => p.MedicalBill)
                    .HasForeignKey<MedicalBill>(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicalBills_Appointments");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<PatientMedicalRecord>(entity =>
            {
                entity.Property(e => e.PatientId).ValueGeneratedNever();

                entity.HasOne(d => d.Patient)
                    .WithOne(p => p.PatientMedicalRecord)
                    .HasForeignKey<PatientMedicalRecord>(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMedicalRecords_Patients");
            });

            modelBuilder.Entity<PersonalInformation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName).IsFixedLength();

                entity.Property(e => e.LastName).IsFixedLength();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PersonalInformation)
                    .HasForeignKey<PersonalInformation>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalInformations_Doctors");

                entity.HasOne(d => d.Id1)
                    .WithOne(p => p.PersonalInformation)
                    .HasForeignKey<PersonalInformation>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalInformations_Patients");

                entity.HasOne(d => d.Id2)
                    .WithOne(p => p.PersonalInformation)
                    .HasForeignKey<PersonalInformation>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalInformations_Staffs");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedules_Doctors");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedules_Services");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_Departments");
            });

            modelBuilder.Entity<ServiceReview>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.ServiceReviews)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceReview_Appointments");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ServiceReviews)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceReview_Doctors");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.ServiceReviews)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceReview_Patients");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceReviews)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceReview_Services");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
