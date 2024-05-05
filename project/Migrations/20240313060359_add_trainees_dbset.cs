using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class add_trainees_dbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_departments_dept_id",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CrsResult_Course_crs_id",
                table: "CrsResult");

            migrationBuilder.DropForeignKey(
                name: "FK_CrsResult_Trainee_trainee_id",
                table: "CrsResult");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_Course_crs_id",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainee_departments_dept_id",
                table: "Trainee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainee",
                table: "Trainee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Trainee",
                newName: "trainees");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "courses");

            migrationBuilder.RenameIndex(
                name: "IX_Trainee_dept_id",
                table: "trainees",
                newName: "IX_trainees_dept_id");

            migrationBuilder.RenameIndex(
                name: "IX_Course_dept_id",
                table: "courses",
                newName: "IX_courses_dept_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainees",
                table: "trainees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_courses",
                table: "courses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_departments_dept_id",
                table: "courses",
                column: "dept_id",
                principalTable: "departments",
                principalColumn: "id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_courses_crs_id",
                table: "instructors",
                column: "crs_id",
                principalTable: "courses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_trainees_departments_dept_id",
                table: "trainees",
                column: "dept_id",
                principalTable: "departments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_departments_dept_id",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CrsResult_courses_crs_id",
                table: "CrsResult");

            migrationBuilder.DropForeignKey(
                name: "FK_CrsResult_trainees_trainee_id",
                table: "CrsResult");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_courses_crs_id",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_trainees_departments_dept_id",
                table: "trainees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainees",
                table: "trainees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_courses",
                table: "courses");

            migrationBuilder.RenameTable(
                name: "trainees",
                newName: "Trainee");

            migrationBuilder.RenameTable(
                name: "courses",
                newName: "Course");

            migrationBuilder.RenameIndex(
                name: "IX_trainees_dept_id",
                table: "Trainee",
                newName: "IX_Trainee_dept_id");

            migrationBuilder.RenameIndex(
                name: "IX_courses_dept_id",
                table: "Course",
                newName: "IX_Course_dept_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainee",
                table: "Trainee",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_departments_dept_id",
                table: "Course",
                column: "dept_id",
                principalTable: "departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrsResult_Course_crs_id",
                table: "CrsResult",
                column: "crs_id",
                principalTable: "Course",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrsResult_Trainee_trainee_id",
                table: "CrsResult",
                column: "trainee_id",
                principalTable: "Trainee",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_Course_crs_id",
                table: "instructors",
                column: "crs_id",
                principalTable: "Course",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainee_departments_dept_id",
                table: "Trainee",
                column: "dept_id",
                principalTable: "departments",
                principalColumn: "id");
        }
    }
}
