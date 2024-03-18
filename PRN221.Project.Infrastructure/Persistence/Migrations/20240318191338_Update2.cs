using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN221.Project.Infrastructure.Persistence.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Schedules",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_Doctors",
                table: "PersonalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_Patients",
                table: "PersonalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReview_Doctors",
                table: "ServiceReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReview_Patients",
                table: "ServiceReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReview_Services",
                table: "ServiceReview");

            migrationBuilder.DropTable(
                name: "AppointmentResults");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceReview",
                table: "ServiceReview");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReview_AppointmentId",
                table: "ServiceReview");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReview_DoctorId",
                table: "ServiceReview");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReview_PatientId",
                table: "ServiceReview");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReview_ServiceId",
                table: "ServiceReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicalRecords",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiceReview");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "ServiceReview");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "ServiceReview");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ServiceReview");

            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "Medications",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "ScheduledDateTime",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Appointments",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ScheduleId",
                table: "Appointments",
                newName: "IX_Appointments_ServiceId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "ServiceReview",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ServiceReview",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ServiceReview",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PersonalInformations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "PatientMedicalRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PatientMedicalRecords",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "PatientMedicalRecords",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "PatientMedicalRecords",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "PatientMedicalRecords",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByStaff",
                table: "PatientMedicalRecords",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "PatientMedicalRecords",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "MedicalBills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "DoctorServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "DoctorServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Appointments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Appointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceReview",
                table: "ServiceReview",
                column: "AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicalRecords_1",
                table: "PatientMedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedicalRecords_UpdatedByStaff",
                table: "PatientMedicalRecords",
                column: "UpdatedByStaff");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedicalRecords_Staffs",
                table: "PatientMedicalRecords",
                column: "UpdatedByStaff",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_Doctors1",
                table: "PersonalInformations",
                column: "Id",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_Patients1",
                table: "PersonalInformations",
                column: "Id",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_Staffs",
                table: "PersonalInformations",
                column: "Id",
                principalTable: "Staff",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedicalRecords_Staffs",
                table: "PatientMedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_Doctors1",
                table: "PersonalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_Patients1",
                table: "PersonalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_Staffs",
                table: "PersonalInformations");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceReview",
                table: "ServiceReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicalRecords_1",
                table: "PatientMedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_PatientMedicalRecords_UpdatedByStaff",
                table: "PatientMedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedByStaff",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "PatientMedicalRecords");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Appointments",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                newName: "IX_Appointments_ScheduleId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "ServiceReview",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ServiceReview",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ServiceReview",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ServiceReview",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "ServiceReview",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "ServiceReview",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "ServiceReview",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PersonalInformations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "PatientMedicalRecords",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "PatientMedicalRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medications",
                table: "PatientMedicalRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "MedicalBills",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "DoctorServices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "DoctorServices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDateTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceReview",
                table: "ServiceReview",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicalRecords",
                table: "PatientMedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateTable(
                name: "AppointmentResults",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestResult = table.Column<string>(type: "text", nullable: true),
                    TreatmentPlan = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentResults", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_AppointmentResults_Appointments",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    NumAppointment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Doctors",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Services",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReview_AppointmentId",
                table: "ServiceReview",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReview_DoctorId",
                table: "ServiceReview",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReview_PatientId",
                table: "ServiceReview",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReview_ServiceId",
                table: "ServiceReview",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ServiceId",
                table: "Schedules",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Schedules",
                table: "Appointments",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_Doctors",
                table: "PersonalInformations",
                column: "Id",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_Patients",
                table: "PersonalInformations",
                column: "Id",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReview_Doctors",
                table: "ServiceReview",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReview_Patients",
                table: "ServiceReview",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReview_Services",
                table: "ServiceReview",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
