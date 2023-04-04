using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotelBookingSystem.Data.Migrations
{
    public partial class addimageToRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "rooms");
        }
    }
}
