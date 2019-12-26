using Microsoft.EntityFrameworkCore.Migrations;

namespace TuDou.Grace.Migrations
{
    public partial class updateDataDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DataDictionary",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDefault",
                table: "DataDictionary",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserNameOrEmailAddress",
                table: "AbpUserLoginAttempts",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256) CHARACTER SET utf8mb4",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AbpSettings",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DataDictionary");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "DataDictionary");

            migrationBuilder.AlterColumn<string>(
                name: "UserNameOrEmailAddress",
                table: "AbpUserLoginAttempts",
                type: "varchar(256) CHARACTER SET utf8mb4",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AbpSettings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);
        }
    }
}
