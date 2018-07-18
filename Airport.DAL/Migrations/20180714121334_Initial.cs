using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AeroplaneTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Places = table.Column<int>(nullable: false),
                    Carrying = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AeroplaneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeparturePoint = table.Column<string>(nullable: true),
                    Destinition = table.Column<string>(nullable: true),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    Experience = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aeroplanes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AeroplaneTypeId = table.Column<Guid>(nullable: true),
                    LifeTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroplanes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aeroplanes_AeroplaneTypes_AeroplaneTypeId",
                        column: x => x.AeroplaneTypeId,
                        principalTable: "AeroplaneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    FlightId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PilotId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crews_Pilots_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    CrewId = table.Column<Guid>(nullable: true),
                    AirplaneId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departures_Aeroplanes_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Aeroplanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departures_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stewardesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    CrewId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stewardesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stewardesses_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aeroplanes_AeroplaneTypeId",
                table: "Aeroplanes",
                column: "AeroplaneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_PilotId",
                table: "Crews",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_AirplaneId",
                table: "Departures",
                column: "AirplaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_CrewId",
                table: "Departures",
                column: "CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_Stewardesses_CrewId",
                table: "Stewardesses",
                column: "CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "Stewardesses");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Aeroplanes");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "AeroplaneTypes");

            migrationBuilder.DropTable(
                name: "Pilots");
        }
    }
}
