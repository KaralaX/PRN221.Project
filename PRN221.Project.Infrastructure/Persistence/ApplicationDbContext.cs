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
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<MedicalBill> MedicalBills => Set<MedicalBill>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<PatientMedicalRecord> PatientMedicalRecords => Set<PatientMedicalRecord>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<ServiceReview> ServiceReviews => Set<ServiceReview>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Appointments_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_Appointments_PatientId");

            entity.HasIndex(e => e.ServiceId, "IX_Appointments_ServiceId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Doctor)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Doctors");

            entity.HasOne(d => d.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Patients");

            entity.HasOne(d => d.Service)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Services");
        });
        
        modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasMaxLength(450);

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

                            j.HasIndex(new[] { "ServiceId" }, "IX_DoctorServices_ServiceId");

                            j.IndexerProperty<Guid>("DoctorId").HasDefaultValueSql("(newid())");

                            j.IndexerProperty<Guid>("ServiceId").HasDefaultValueSql("(newid())");
                        });
            });

            modelBuilder.Entity<MedicalBill>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);

                entity.Property(e => e.AppointmentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Appointment)
                    .WithOne(p => p.MedicalBill)
                    .HasForeignKey<MedicalBill>(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicalBills_Appointments");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<PatientMedicalRecord>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK_PatientMedicalRecords_1");

                entity.HasIndex(e => e.UpdatedByStaff, "IX_PatientMedicalRecords_UpdatedByStaff");

                entity.Property(e => e.PatientId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Patient)
                    .WithOne(p => p.PatientMedicalRecord)
                    .HasForeignKey<PatientMedicalRecord>(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMedicalRecords_Patients");

                entity.HasOne(d => d.UpdatedByStaffNavigation)
                    .WithMany(p => p.PatientMedicalRecords)
                    .HasForeignKey(d => d.UpdatedByStaff)
                    .HasConstraintName("FK_PatientMedicalRecords_Staffs");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId, "IX_Services_DepartmentId");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

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
                entity.HasKey(e => e.AppointmentId);

                entity.ToTable("ServiceReview");

                entity.Property(e => e.AppointmentId).ValueGeneratedNever();

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Appointment)
                    .WithOne(p => p.ServiceReview)
                    .HasForeignKey<ServiceReview>(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceReview_Appointments");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasMaxLength(450);
            });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    
        optionsBuilder.UseSqlServer("Server=(local);Database=PRN221.Project;User=sa;Password=12345678;");
    }
}