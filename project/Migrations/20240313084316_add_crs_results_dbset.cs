using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class add_crs_results_dbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrsResult_courses_crs_id",
                table: "CrsResult");

            migrationBuilder.DropForeignKey(
                name: "FK_CrsResult_trainees_trainee_id",
                table: "CrsResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrsResult",
                table: "CrsResult");

            migrationBuilder.RenameTable(
                name: "CrsResult",
                newName: "crsResults");

            migrationBuilder.RenameIndex(
                name: "IX_CrsResult_trainee_id",
                table: "crsResults",
                newName: "IX_crsResults_trainee_id");

            migrationBuilder.RenameIndex(
                name: "IX_CrsResult_crs_id",
                table: "crsResults",
                newName: "IX_crsResults_crs_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_crsResults",
                table: "crsResults",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_crsResults_courses_crs_id",
                table: "crsResults",
                column: "crs_id",
                principalTable: "courses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_crsResults_trainees_trainee_id",
                table: "crsResults",
                column: "trainee_id",
                principalTable: "trainees",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_crsResults_courses_crs_id",
                table: "crsResults");

            migrationBuilder.DropForeignKey(
                name: "FK_crsResults_trainees_trainee_id",
                table: "crsResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_crsResults",
                table: "crsResults");

            migrationBuilder.RenameTable(
                name: "crsResults",
                newName: "CrsResult");

            migrationBuilder.RenameIndex(
                name: "IX_crsResults_trainee_id",
                table: "CrsResult",
                newName: "IX_CrsResult_trainee_id");

            migrationBuilder.RenameIndex(
                name: "IX_crsResults_crs_id",
                table: "CrsResult",
                newName: "IX_CrsResult_crs_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrsResult",
                table: "CrsResult",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrsResult_courses_crs_id",
                table: "CrsResult",
                column: "crs_id",
                principalTable: "courses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrsResult_trainees_trainee_id",
                table: "CrsResult",
                column: "trainee_id",
                principalTable: "trainees",
                principalColumn: "id");
        }
    }
}
