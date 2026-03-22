using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenCityManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    ID_district = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.ID_district);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID_employee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID_employee);
                });

            migrationBuilder.CreateTable(
                name: "PlantType",
                columns: table => new
                {
                    ID_plant_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantType", x => x.ID_plant_type);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID_role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID_role);
                });

            migrationBuilder.CreateTable(
                name: "WorkType",
                columns: table => new
                {
                    ID_work_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkType", x => x.ID_work_type);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    ID_plant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_plant_type = table.Column<int>(type: "int", nullable: false),
                    ID_district = table.Column<int>(type: "int", nullable: false),
                    Planting_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Health_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.ID_plant);
                    table.ForeignKey(
                        name: "FK_Plant_District_ID_district",
                        column: x => x.ID_district,
                        principalTable: "District",
                        principalColumn: "ID_district",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plant_PlantType_ID_plant_type",
                        column: x => x.ID_plant_type,
                        principalTable: "PlantType",
                        principalColumn: "ID_plant_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    ID_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.ID_user);
                    table.ForeignKey(
                        name: "FK_AppUser_Role_ID_role",
                        column: x => x.ID_role,
                        principalTable: "Role",
                        principalColumn: "ID_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    ID_maintenance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_plant = table.Column<int>(type: "int", nullable: false),
                    ID_work_type = table.Column<int>(type: "int", nullable: false),
                    ID_employee = table.Column<int>(type: "int", nullable: false),
                    Work_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.ID_maintenance);
                    table.ForeignKey(
                        name: "FK_Maintenance_Employee_ID_employee",
                        column: x => x.ID_employee,
                        principalTable: "Employee",
                        principalColumn: "ID_employee",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenance_Plant_ID_plant",
                        column: x => x.ID_plant,
                        principalTable: "Plant",
                        principalColumn: "ID_plant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenance_WorkType_ID_work_type",
                        column: x => x.ID_work_type,
                        principalTable: "WorkType",
                        principalColumn: "ID_work_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plant_Employee",
                columns: table => new
                {
                    ID_plant_employee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_plant = table.Column<int>(type: "int", nullable: false),
                    ID_employee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant_Employee", x => x.ID_plant_employee);
                    table.ForeignKey(
                        name: "FK_Plant_Employee_Employee_ID_employee",
                        column: x => x.ID_employee,
                        principalTable: "Employee",
                        principalColumn: "ID_employee",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plant_Employee_Plant_ID_plant",
                        column: x => x.ID_plant,
                        principalTable: "Plant",
                        principalColumn: "ID_plant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantPassport",
                columns: table => new
                {
                    ID_passport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_plant = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Last_inspection_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantPassport", x => x.ID_passport);
                    table.ForeignKey(
                        name: "FK_PlantPassport_Plant_ID_plant",
                        column: x => x.ID_plant,
                        principalTable: "Plant",
                        principalColumn: "ID_plant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID_role", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Полный доступ ко всем функциям", "Администратор" },
                    { 2, "Просмотр и добавление обслуживания", "Работник" },
                    { 3, "Справочники, растения и отчёты", "Менеджер по учёту" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "ID_user", "FullName", "ID_role", "Login", "PasswordHash" },
                values: new object[] { 1, "Администратор системы", 1, "admin", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_ID_role",
                table: "AppUser",
                column: "ID_role");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ID_employee",
                table: "Maintenance",
                column: "ID_employee");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ID_plant",
                table: "Maintenance",
                column: "ID_plant");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ID_work_type",
                table: "Maintenance",
                column: "ID_work_type");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_ID_district",
                table: "Plant",
                column: "ID_district");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_ID_plant_type",
                table: "Plant",
                column: "ID_plant_type");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_Employee_ID_employee",
                table: "Plant_Employee",
                column: "ID_employee");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_Employee_ID_plant",
                table: "Plant_Employee",
                column: "ID_plant");

            migrationBuilder.CreateIndex(
                name: "IX_PlantPassport_ID_plant",
                table: "PlantPassport",
                column: "ID_plant",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "Plant_Employee");

            migrationBuilder.DropTable(
                name: "PlantPassport");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "WorkType");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "PlantType");
        }
    }
}
