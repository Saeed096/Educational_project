using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class is_deleted_course : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 1,
                column: "is_deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 2,
                column: "is_deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 3,
                column: "is_deleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "courses");
        }
    }
}
