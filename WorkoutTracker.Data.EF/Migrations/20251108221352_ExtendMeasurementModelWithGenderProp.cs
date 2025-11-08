using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class ExtendMeasurementModelWithGenderProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Measurements");
        }
    }
}
