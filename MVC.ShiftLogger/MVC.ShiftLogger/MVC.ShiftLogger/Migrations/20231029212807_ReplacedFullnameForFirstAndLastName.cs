using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.ShiftLogger.Migrations
{
    public partial class ReplacedFullnameForFirstAndLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Shifts",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Shifts",
                newName: "FullName");
        }
    }
}
