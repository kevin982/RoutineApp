using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CategoryMS_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("6f1236ed-bb75-4503-8fbb-3aaf916fd682"), "Legs" },
                    { new Guid("9b617966-f433-4666-b91c-f1308fe7250d"), "Abs" },
                    { new Guid("d20436a6-493c-4752-97af-cbf54efac614"), "Chest" },
                    { new Guid("190706f5-47ac-4fdd-916b-f26ff05c8b6c"), "Shoulders" },
                    { new Guid("d8983703-3b47-4db8-a4c9-24e132444f20"), "Biceps" },
                    { new Guid("84fdb065-8c4a-464d-9fcf-3b977e79cf35"), "Triceps" },
                    { new Guid("61574778-0381-4cfc-a18a-58d67876284b"), "Forearms" },
                    { new Guid("3f3ab9f7-7930-4735-affc-8bb7ec9a1e0e"), "Back" },
                    { new Guid("87ffcaa0-657f-4a25-a4f9-5be94cdda8d4"), "Cardio" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
