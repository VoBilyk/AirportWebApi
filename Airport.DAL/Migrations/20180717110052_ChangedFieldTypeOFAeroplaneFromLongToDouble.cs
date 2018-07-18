using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.DAL.Migrations
{
    public partial class ChangedFieldTypeOFAeroplaneFromLongToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LifeTimeHourses",
                table: "Aeroplanes",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "LifeTimeHourses",
                table: "Aeroplanes",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
