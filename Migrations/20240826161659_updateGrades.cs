using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class updateGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12086b14-d912-4247-b939-4d5a175ce2f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "515d494b-12c5-4e56-ac8a-c695b7722e8d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5796bcd5-15fe-4a7f-be59-dbedbf289f39", null, "User", "USER" },
                    { "c079f73d-eb0e-4b32-a304-9998ab016a17", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId_GradeItemId",
                table: "Grades",
                columns: new[] { "StudentId", "GradeItemId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId_GradeItemId",
                table: "Grades");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5796bcd5-15fe-4a7f-be59-dbedbf289f39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c079f73d-eb0e-4b32-a304-9998ab016a17");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12086b14-d912-4247-b939-4d5a175ce2f4", null, "Admin", "ADMIN" },
                    { "515d494b-12c5-4e56-ac8a-c695b7722e8d", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");
        }
    }
}
