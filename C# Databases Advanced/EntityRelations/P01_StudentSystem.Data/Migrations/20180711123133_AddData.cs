using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Data.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Description", "EndDate", "Name", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, "C# Advanced", new DateTime(2018, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "C#", 330m, new DateTime(2018, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Java Advanced", new DateTime(2018, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java", 330m, new DateTime(2018, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Birthday", "Name", "PhoneNumber", "RegisteredOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pesho", "0123456789", new DateTime(2018, 7, 11, 15, 31, 31, 974, DateTimeKind.Local) },
                    { 2, new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peshka", "0123456789", new DateTime(2018, 7, 11, 15, 31, 31, 976, DateTimeKind.Local) },
                    { 3, new DateTime(1985, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gosho", "0123456789", new DateTime(2018, 7, 11, 15, 31, 31, 976, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ResourceId", "CourseId", "Name", "ResourceType", "Url" },
                values: new object[,]
                {
                    { 1, 1, "C#", 2, "url" },
                    { 2, 1, "C# Video", 0, "url" }
                });

            migrationBuilder.InsertData(
                table: "StudentCourses",
                columns: new[] { "StudentId", "CourseId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);
        }
    }
}
