using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSittingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UniqueRegNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegNo",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Students_RegNo",
                table: "Students",
                column: "RegNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_RegNo",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "RegNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
