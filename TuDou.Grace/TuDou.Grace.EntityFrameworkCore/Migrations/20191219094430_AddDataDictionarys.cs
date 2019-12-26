using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuDou.Grace.Migrations
{
    public partial class AddDataDictionarys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataDictionary",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataDictionary_DataDictionary_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DataDictionary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataDictionary_ParentId",
                table: "DataDictionary",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataDictionary");
        }
    }
}
