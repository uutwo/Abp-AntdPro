using Microsoft.EntityFrameworkCore.Migrations;

namespace TuDou.Grace.Migrations
{
    public partial class addField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DataDictionary",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCategory",
                table: "DataDictionary",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DataDictionary");

            migrationBuilder.DropColumn(
                name: "IsCategory",
                table: "DataDictionary");
        }
    }
}
