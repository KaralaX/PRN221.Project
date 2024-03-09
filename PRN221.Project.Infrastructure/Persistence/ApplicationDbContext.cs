using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Identity;

namespace PRN221.Project.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; } = null!;
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentResult> AppointmentResults { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<MedicalBill> MedicalBills { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<PatientMedicalRecord> PatientMedicalRecords { get; set; } = null!;
    public DbSet<PersonalInformation> PersonalInformations { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ServiceReview> ServiceReviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasIndex(e => e.PatientId, "IX_Appointments_PatientId");

            entity.HasIndex(e => e.ScheduleId, "IX_Appointments_ScheduleId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Status).HasMaxLength(50);

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
            entity.HasKey(e => e.AppointmentId);

            entity.Property(e => e.AppointmentId).ValueGeneratedNever();

            entity.Property(e => e.TestResult).HasColumnType("text");

            entity.Property(e => e.TreatmentPlan).HasColumnType("text");

            entity.HasOne(d => d.Appointment)
                .WithOne(p => p.AppointmentResult)
                .HasForeignKey<AppointmentResult>(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentResults_Appointments");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Description).HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasMany(d => d.Services)
                .WithMany(p => p.Doctors)
                .UsingEntity<Dictionary<string, object>>(
                    "DoctorService",
                    l => l.HasOne<Service>().WithMany().HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DoctorServices_Services"),
                    r => r.HasOne<Doctor>().WithMany().HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DoctorServices_Doctors"),
                    j =>
                    {
                        j.HasKey("DoctorId", "ServiceId");

                        j.ToTable("DoctorServices");

                        j.HasIndex(new[] { "ServiceId" }, "IX_DoctorServices_ServiceId");
                    });
        });

        modelBuilder.Entity<MedicalBill>(entity =>
        {
            entity.HasKey(e => e.AppointmentId);

            entity.Property(e => e.AppointmentId).ValueGeneratedNever();

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Appointment)
                .WithOne(p => p.MedicalBill)
                .HasForeignKey<MedicalBill>(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalBills_Appointments");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<PatientMedicalRecord>(entity =>
        {
            entity.HasKey(e => e.PatientId);

            entity.Property(e => e.PatientId).ValueGeneratedNever();

            entity.Property(e => e.Allergies).HasColumnType("text");

            entity.Property(e => e.Medications).HasColumnType("text");

            entity.HasOne(d => d.Patient)
                .WithOne(p => p.PatientMedicalRecord)
                .HasForeignKey<PatientMedicalRecord>(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientMedicalRecords_Patients");
        });

        modelBuilder.Entity<PersonalInformation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Address).HasMaxLength(100);

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.Property(e => e.Gender).HasMaxLength(20);

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsFixedLength();

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
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Schedules_DoctorId");

            entity.HasIndex(e => e.ServiceId, "IX_Schedules_ServiceId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.DateTime).HasColumnType("datetime");

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
            entity.HasIndex(e => e.DepartmentId, "IX_Services_DepartmentId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Description).HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Services)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Services_Departments");
        });

        modelBuilder.Entity<ServiceReview>(entity =>
        {
            entity.ToTable("ServiceReview");

            entity.HasIndex(e => e.AppointmentId, "IX_ServiceReview_AppointmentId");

            entity.HasIndex(e => e.DoctorId, "IX_ServiceReview_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_ServiceReview_PatientId");

            entity.HasIndex(e => e.ServiceId, "IX_ServiceReview_ServiceId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Comment).HasMaxLength(500);

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
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Server=(local);Database=PRN221.Project;User=sa;Password=12345678;");
    }
}