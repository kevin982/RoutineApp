using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineCoreApp.Migrations
{
    public partial class NowTheMailsAreObtainedByDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailHtmlCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mails");
        }
    }
}
