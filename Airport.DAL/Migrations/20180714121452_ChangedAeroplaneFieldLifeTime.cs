using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.DAL.Migrations
{
    public partial class ChangedAeroplaneFieldLifeTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifeTime",
                table: "Aeroplanes");

            migrationBuilder.AddColumn<long>(
                name: "LifeTimeHourses",
                table: "Aeroplanes",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifeTimeHourses",
                table: "Aeroplanes");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LifeTime",
                table: "Aeroplanes",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
