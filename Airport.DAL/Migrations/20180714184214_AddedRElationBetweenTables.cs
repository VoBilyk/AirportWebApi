using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.DAL.Migrations
{
    public partial class AddedRElationBetweenTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aeroplanes_AeroplaneTypes_AeroplaneTypeId",
                table: "Aeroplanes");

            migrationBuilder.DropForeignKey(
                name: "FK_Crews_Pilots_PilotId",
                table: "Crews");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Aeroplanes_AirplaneId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Stewardesses_Crews_CrewId",
                table: "Stewardesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "CrewId",
                table: "Departures",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "AirplaneId",
                table: "Departures",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PilotId",
                table: "Crews",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Aeroplanes_AeroplaneTypes_AeroplaneTypeId",
                table: "Aeroplanes",
                column: "AeroplaneTypeId",
                principalTable: "AeroplaneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_Pilots_PilotId",
                table: "Crews",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Aeroplanes_AirplaneId",
                table: "Departures",
                column: "AirplaneId",
                principalTable: "Aeroplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stewardesses_Crews_CrewId",
                table: "Stewardesses",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aeroplanes_AeroplaneTypes_AeroplaneTypeId",
                table: "Aeroplanes");

            migrationBuilder.DropForeignKey(
                name: "FK_Crews_Pilots_PilotId",
                table: "Crews");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Aeroplanes_AirplaneId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Stewardesses_Crews_CrewId",
                table: "Stewardesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "CrewId",
                table: "Departures",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AirplaneId",
                table: "Departures",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PilotId",
                table: "Crews",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aeroplanes_AeroplaneTypes_AeroplaneTypeId",
                table: "Aeroplanes",
                column: "AeroplaneTypeId",
                principalTable: "AeroplaneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_Pilots_PilotId",
                table: "Crews",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Aeroplanes_AirplaneId",
                table: "Departures",
                column: "AirplaneId",
                principalTable: "Aeroplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stewardesses_Crews_CrewId",
                table: "Stewardesses",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
