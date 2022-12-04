using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurboRentingv2.Api.Migrations
{
    public partial class AddedContractCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractCode",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractCode",
                table: "Contracts");
        }
    }
}
