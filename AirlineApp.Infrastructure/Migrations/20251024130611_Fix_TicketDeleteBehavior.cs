using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Fix_TicketDeleteBehavior : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Flights_FlightId",
            table: "Tickets");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Passengers_PassengerId",
            table: "Tickets");

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Flights_FlightId",
            table: "Tickets",
            column: "FlightId",
            principalTable: "Flights",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Passengers_PassengerId",
            table: "Tickets",
            column: "PassengerId",
            principalTable: "Passengers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Flights_FlightId",
            table: "Tickets");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Passengers_PassengerId",
            table: "Tickets");

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Flights_FlightId",
            table: "Tickets",
            column: "FlightId",
            principalTable: "Flights",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Passengers_PassengerId",
            table: "Tickets",
            column: "PassengerId",
            principalTable: "Passengers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
