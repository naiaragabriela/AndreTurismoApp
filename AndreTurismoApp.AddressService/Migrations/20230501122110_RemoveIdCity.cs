using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndreTurismoApp.AddressService.Migrations
{
    public partial class RemoveIdCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCity",
                table: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCity",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
