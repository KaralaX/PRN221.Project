using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN221.Project.Infrastructure.Persistence.Migrations
{
    public partial class UpdateDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_Staffs",
                table: "PersonalInformations");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "Services",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_Staffs",
                table: "PersonalInformations",
                column: "Id",
                principalTable: "Staffs",
                principalColumn: "Id");
        }
    }
}
