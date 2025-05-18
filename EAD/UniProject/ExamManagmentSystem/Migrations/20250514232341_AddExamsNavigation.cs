using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddExamsNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Batches_BatchId",
                table: "Exams");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Batches_BatchId",
                table: "Exams",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Batches_BatchId",
                table: "Exams");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Batches_BatchId",
                table: "Exams",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
