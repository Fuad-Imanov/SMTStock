using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMTstock.DAL.Migrations
{
    public partial class totalpricefieldtoordermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderProducts",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderProducts");
        }
    }
}
