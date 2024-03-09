﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PRN221.Project.Infrastructure.Persistence;

#nullable disable

namespace PRN221.Project.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240302043054_UpdateEntity")]
    partial class UpdateEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DoctorService", b =>
                {
                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DoctorId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("DoctorServices", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ScheduledDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.AppointmentResult", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TestResult")
                        .HasColumnType("text");

                    b.Property<string>("TreatmentPlan")
                        .HasColumnType("text");

                    b.HasKey("AppointmentId");

                    b.ToTable("AppointmentResults");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.MedicalBill", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("AppointmentId");

                    b.ToTable("MedicalBills");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.PatientMedicalRecord", b =>
                {
                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Allergies")
                        .HasColumnType("text");

                    b.Property<string>("Medications")
                        .HasColumnType("text");

                    b.HasKey("PatientId");

                    b.ToTable("PatientMedicalRecords");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.PersonalInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<string>("Gender")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("PersonalInformations");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumAppointment")
                        .HasColumnType("int");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.ServiceReview", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceReview");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("PRN221.Project.Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("DoctorService", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .IsRequired()
                        .HasConstraintName("FK_DoctorServices_Doctors");

                    b.HasOne("PRN221.Project.Domain.Entities.Service", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK_DoctorServices_Services");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Appointment", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .IsRequired()
                        .HasConstraintName("FK_Appointments_Patients");

                    b.HasOne("PRN221.Project.Domain.Entities.Schedule", "Schedule")
                        .WithMany("Appointments")
                        .HasForeignKey("ScheduleId")
                        .IsRequired()
                        .HasConstraintName("FK_Appointments_Schedules");

                    b.Navigation("Patient");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.AppointmentResult", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Appointment", "Appointment")
                        .WithOne("AppointmentResult")
                        .HasForeignKey("PRN221.Project.Domain.Entities.AppointmentResult", "AppointmentId")
                        .IsRequired()
                        .HasConstraintName("FK_AppointmentResults_Appointments");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.MedicalBill", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Appointment", "Appointment")
                        .WithOne("MedicalBill")
                        .HasForeignKey("PRN221.Project.Domain.Entities.MedicalBill", "AppointmentId")
                        .IsRequired()
                        .HasConstraintName("FK_MedicalBills_Appointments");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.PatientMedicalRecord", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Patient", "Patient")
                        .WithOne("PatientMedicalRecord")
                        .HasForeignKey("PRN221.Project.Domain.Entities.PatientMedicalRecord", "PatientId")
                        .IsRequired()
                        .HasConstraintName("FK_PatientMedicalRecords_Patients");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.PersonalInformation", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Doctor", "IdNavigation")
                        .WithOne("PersonalInformation")
                        .HasForeignKey("PRN221.Project.Domain.Entities.PersonalInformation", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_PersonalInformations_Doctors");

                    b.HasOne("PRN221.Project.Domain.Entities.Patient", "Id1")
                        .WithOne("PersonalInformation")
                        .HasForeignKey("PRN221.Project.Domain.Entities.PersonalInformation", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_PersonalInformations_Patients");

                    b.HasOne("PRN221.Project.Domain.Entities.Staff", "Id2")
                        .WithOne("PersonalInformation")
                        .HasForeignKey("PRN221.Project.Domain.Entities.PersonalInformation", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_PersonalInformations_Staffs");

                    b.Navigation("Id1");

                    b.Navigation("Id2");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Schedule", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Doctor", "Doctor")
                        .WithMany("Schedules")
                        .HasForeignKey("DoctorId")
                        .IsRequired()
                        .HasConstraintName("FK_Schedules_Doctors");

                    b.HasOne("PRN221.Project.Domain.Entities.Service", "Service")
                        .WithMany("Schedules")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK_Schedules_Services");

                    b.Navigation("Doctor");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Service", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Department", "Department")
                        .WithMany("Services")
                        .HasForeignKey("DepartmentId")
                        .IsRequired()
                        .HasConstraintName("FK_Services_Departments");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.ServiceReview", b =>
                {
                    b.HasOne("PRN221.Project.Domain.Entities.Appointment", "Appointment")
                        .WithMany("ServiceReviews")
                        .HasForeignKey("AppointmentId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceReview_Appointments");

                    b.HasOne("PRN221.Project.Domain.Entities.Doctor", "Doctor")
                        .WithMany("ServiceReviews")
                        .HasForeignKey("DoctorId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceReview_Doctors");

                    b.HasOne("PRN221.Project.Domain.Entities.Patient", "Patient")
                        .WithMany("ServiceReviews")
                        .HasForeignKey("PatientId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceReview_Patients");

                    b.HasOne("PRN221.Project.Domain.Entities.Service", "Service")
                        .WithMany("ServiceReviews")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK_ServiceReview_Services");

                    b.Navigation("Appointment");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Appointment", b =>
                {
                    b.Navigation("AppointmentResult");

                    b.Navigation("MedicalBill");

                    b.Navigation("ServiceReviews");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Department", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Doctor", b =>
                {
                    b.Navigation("PersonalInformation");

                    b.Navigation("Schedules");

                    b.Navigation("ServiceReviews");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PatientMedicalRecord");

                    b.Navigation("PersonalInformation");

                    b.Navigation("ServiceReviews");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Schedule", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Service", b =>
                {
                    b.Navigation("Schedules");

                    b.Navigation("ServiceReviews");
                });

            modelBuilder.Entity("PRN221.Project.Domain.Entities.Staff", b =>
                {
                    b.Navigation("PersonalInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
