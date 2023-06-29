using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class CreateDatabaseForSimpleEmailService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "Name", "UpdatedAt", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 12, 16, 24, 8, 942, DateTimeKind.Utc).AddTicks(3142), 1, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, new DateTime(2023, 3, 12, 16, 24, 8, 942, DateTimeKind.Utc).AddTicks(3165), 1, "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "Email", "Hash", "RoleId", "Salt", "UpdatedAt", "UpdatedUserId", "Username" },
                values: new object[] { 1, new DateTime(2023, 3, 12, 16, 24, 8, 942, DateTimeKind.Utc).AddTicks(3801), 1, "tahmazlisanan2022@gmail.com", "�J*\"����D�������G9c�0������\"�`�", 1, "mhZUdG1W2386Vt5px6eF8YDy40vlpYyqF69t9PdR0uAr8pDtOdgjVD9qNJjeW380W7q0nKalIaDEHEnbMWaZvuLH+Yuge+5TDeSJ8tPwZXuG8bcsJVFCN6nog3rybAS1CpCIdMPTCaCl0d3Rk6wGH9A+ad9jZv8lG3YWyQa+bfwKUE+TR+h/SaUbJtO7+2IXSyufxol+wcjYg4pE16yH5uHkGOAiW11ibGEhxQwule5aIrob2mfOZzrJZTgvLi4p3Rya4aPY6GNLp7BpF8KRCVpJU16Jgx26S65rpq2q1IQFT8qZEoJOc51jJqmMfJvsjs+BT1pXWZtO++ERJjOs", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}