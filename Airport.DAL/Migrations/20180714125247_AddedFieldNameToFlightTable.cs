using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.DAL.Migrations
{
    public partial class AddedFieldNameToFlightTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Flights",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Flights");
        }
    }
}
