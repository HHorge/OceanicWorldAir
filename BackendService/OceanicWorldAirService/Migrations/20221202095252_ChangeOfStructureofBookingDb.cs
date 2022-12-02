using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OceanicWorldAirService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfStructureofBookingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "JsonRoute",
                table: "Bookings",
                newName: "StartDestination");

            migrationBuilder.RenameColumn(
                name: "JsonParcel",
                table: "Bookings",
                newName: "Price");

            migrationBuilder.AlterColumn<float>(
                name: "Time",
                table: "Bookings",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "EndDestination",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JsonParcelsList",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDestination",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "JsonParcelsList",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "StartDestination",
                table: "Bookings",
                newName: "JsonRoute");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Bookings",
                newName: "JsonParcel");

            migrationBuilder.AlterColumn<int>(
                name: "Time",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
