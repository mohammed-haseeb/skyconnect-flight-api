using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservation.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flights_FlightId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FlightId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "FlightId1",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightId",
                table: "Reservations",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flights_FlightId",
                table: "Reservations",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Flights_FlightId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FlightId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "FlightId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FlightId1",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightId1",
                table: "Reservations",
                column: "FlightId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Flights_FlightId1",
                table: "Reservations",
                column: "FlightId1",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
