using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xarajat.Data.Migrations
{
    public partial class roomkeylength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "rooms",
                type: "nvarchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "rooms",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)");
        }
    }
}
