using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatingDataBasesAndTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "identitysSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ProfileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identitysSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "servicesProvideds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicesProvideds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IdentitySystemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_identitysSystem_IdentitySystemId",
                        column: x => x.IdentitySystemId,
                        principalTable: "identitysSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HiringDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DismissalDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IdentitySystemId = table.Column<int>(type: "int", nullable: false),
                    WorkScheduleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_identitysSystem_IdentitySystemId",
                        column: x => x.IdentitySystemId,
                        principalTable: "identitysSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedulings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalServiceTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalValue = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedulings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedulings_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_schedulings_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "workSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workSchedules_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedulingServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchedulingId = table.Column<int>(type: "int", nullable: false),
                    ServiceProvidedId = table.Column<int>(type: "int", nullable: false),
                    PriceAtMoment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationAtMoment = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedulingServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedulingServices_schedulings_SchedulingId",
                        column: x => x.SchedulingId,
                        principalTable: "schedulings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedulingServices_servicesProvideds_ServiceProvidedId",
                        column: x => x.ServiceProvidedId,
                        principalTable: "servicesProvideds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_CPF",
                table: "customers",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_IdentitySystemId",
                table: "customers",
                column: "IdentitySystemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_identitysSystem_Email",
                table: "identitysSystem",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_schedulings_CustomerId",
                table: "schedulings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_schedulings_UserId",
                table: "schedulings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_schedulingServices_SchedulingId",
                table: "schedulingServices",
                column: "SchedulingId");

            migrationBuilder.CreateIndex(
                name: "IX_schedulingServices_ServiceProvidedId",
                table: "schedulingServices",
                column: "ServiceProvidedId");

            migrationBuilder.CreateIndex(
                name: "IX_users_CPF",
                table: "users",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_IdentitySystemId",
                table: "users",
                column: "IdentitySystemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_UserId",
                table: "workSchedules",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedulingServices");

            migrationBuilder.DropTable(
                name: "workSchedules");

            migrationBuilder.DropTable(
                name: "schedulings");

            migrationBuilder.DropTable(
                name: "servicesProvideds");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "identitysSystem");
        }
    }
}
