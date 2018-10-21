using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftUniCopy.Data.Migrations
{
    public partial class Restrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LecturerId",
                table: "CourseInstances",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LecturerId",
                table: "CourseInstances",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
