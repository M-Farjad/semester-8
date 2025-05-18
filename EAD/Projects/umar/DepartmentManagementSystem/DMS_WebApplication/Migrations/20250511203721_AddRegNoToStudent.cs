using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSittingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddRegNoToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegNo",
                table: "Students");
        }
    }
}
