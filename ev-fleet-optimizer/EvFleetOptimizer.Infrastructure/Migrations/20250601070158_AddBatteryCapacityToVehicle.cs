using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvFleetOptimizer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBatteryCapacityToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BatteryCapacityKWh",
                table: "Vehicles",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryCapacityKWh",
                table: "Vehicles");
        }
    }
}
