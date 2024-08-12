using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class finalGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d8245d5-fe8f-4cb7-a899-07167f11eb17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7f07408-6bb9-4f26-bc13-4fbe2e13462f");

            migrationBuilder.CreateTable(
                name: "FinalGrades",
                columns: table => new
                {
                    FinalGradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalGrades", x => x.FinalGradeId);
                    table.ForeignKey(
                        name: "FK_FinalGrades_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12086b14-d912-4247-b939-4d5a175ce2f4", null, "Admin", "ADMIN" },
                    { "515d494b-12c5-4e56-ac8a-c695b7722e8d", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrades_ClassId",
                table: "FinalGrades",
                column: "ClassId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalGrades");

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
                    { "3d8245d5-fe8f-4cb7-a899-07167f11eb17", null, "User", "USER" },
                    { "b7f07408-6bb9-4f26-bc13-4fbe2e13462f", null, "Admin", "ADMIN" }
                });
        }
    }
}
