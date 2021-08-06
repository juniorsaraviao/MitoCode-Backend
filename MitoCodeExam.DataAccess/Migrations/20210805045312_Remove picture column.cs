using Microsoft.EntityFrameworkCore.Migrations;

namespace MitoCodeExam.DataAccess.Migrations
{
    public partial class Removepicturecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
