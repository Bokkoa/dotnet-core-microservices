using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopServices.Api.Book.Migrations
{
    public partial class SqlServerInitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookMaterial",
                columns: table => new
                {
                    BookMaterialId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    AuthorBook = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMaterial", x => x.BookMaterialId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookMaterial");
        }
    }
}
