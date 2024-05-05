using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    manager = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    degree = table.Column<double>(type: "float", nullable: false),
                    minDegree = table.Column<double>(type: "float", nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    dept_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.id);
                    table.ForeignKey(
                        name: "FK_Course_departments_dept_id",
                        column: x => x.dept_id,
                        principalTable: "departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Trainee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dept_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainee", x => x.id);
                    table.ForeignKey(
                        name: "FK_Trainee_departments_dept_id",
                        column: x => x.dept_id,
                        principalTable: "departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "instructors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    salary = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    crs_id = table.Column<int>(type: "int", nullable: true),
                    dept_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructors", x => x.id);
                    table.ForeignKey(
                        name: "FK_instructors_Course_crs_id",
                        column: x => x.crs_id,
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_instructors_departments_dept_id",
                        column: x => x.dept_id,
                        principalTable: "departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CrsResult",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    degree = table.Column<double>(type: "float", nullable: false),
                    crs_id = table.Column<int>(type: "int", nullable: true),
                    trainee_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrsResult", x => x.id);
                    table.ForeignKey(
                        name: "FK_CrsResult_Course_crs_id",
                        column: x => x.crs_id,
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CrsResult_Trainee_trainee_id",
                        column: x => x.trainee_id,
                        principalTable: "Trainee",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "id", "degree", "dept_id", "hours", "minDegree", "name" },
                values: new object[,]
                {
                    { 1, 100.0, null, 0, 60.0, "C#" },
                    { 2, 100.0, null, 0, 70.0, "OOP" },
                    { 3, 100.0, null, 0, 50.0, "J.S" }
                });

            migrationBuilder.InsertData(
                table: "CrsResult",
                columns: new[] { "id", "crs_id", "degree", "trainee_id" },
                values: new object[,]
                {
                    { 1, null, 90.0, null },
                    { 2, null, 90.0, null },
                    { 3, null, 90.0, null }
                });

            migrationBuilder.InsertData(
                table: "Trainee",
                columns: new[] { "id", "address", "dept_id", "grade", "image", "name" },
                values: new object[,]
                {
                    { 1, "cairo", null, "excellent", "stu1.jpg", "shady" },
                    { 2, "Alex", null, "good", "stu2.webp", "moustafa" },
                    { 3, "Minya", null, "failed", "stu3.webp", "Mona" }
                });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "id", "manager", "name" },
                values: new object[,]
                {
                    { 1, "Eng.Ayman", "SD" },
                    { 2, "Eng.Saad", "UI/UX" },
                    { 3, "Eng.christen", "open source" }
                });

            migrationBuilder.InsertData(
                table: "instructors",
                columns: new[] { "id", "address", "crs_id", "dept_id", "image", "name", "salary" },
                values: new object[,]
                {
                    { 1, "cairo", null, null, "ins1.png", "Mohamed", 5000 },
                    { 2, "Alex", null, null, "ins2.webp", "Ahmed", 4000 },
                    { 3, "Minya", null, null, "ins3.webp", "Josphine", 40000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_dept_id",
                table: "Course",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "IX_CrsResult_crs_id",
                table: "CrsResult",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_CrsResult_trainee_id",
                table: "CrsResult",
                column: "trainee_id");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_crs_id",
                table: "instructors",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_dept_id",
                table: "instructors",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trainee_dept_id",
                table: "Trainee",
                column: "dept_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrsResult");

            migrationBuilder.DropTable(
                name: "instructors");

            migrationBuilder.DropTable(
                name: "Trainee");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
