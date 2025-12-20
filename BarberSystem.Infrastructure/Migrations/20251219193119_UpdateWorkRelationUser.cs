using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkRelationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkScheduleId",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkScheduleId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
