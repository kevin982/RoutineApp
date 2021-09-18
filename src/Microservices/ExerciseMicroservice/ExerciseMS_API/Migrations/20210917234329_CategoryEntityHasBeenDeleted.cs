using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExerciseMS_API.Migrations
{
    public partial class CategoryEntityHasBeenDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Categories_CategoryId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("04b1e0cd-6f9f-4bf3-bf28-f9487fd588ed"), "Legs" },
                    { new Guid("786f4325-63e7-4f3d-9aa9-4355538a3ba3"), "Abs" },
                    { new Guid("5f5b42b2-e14d-4824-8d93-3977e3355f01"), "Chest" },
                    { new Guid("d5e61185-b9ce-4acc-a801-896ed3737f65"), "Shoulders" },
                    { new Guid("0caffb7d-defe-46c4-8082-5ab3a7ad3f89"), "Biceps" },
                    { new Guid("cea33567-29de-41d7-8689-79d2a0bdb67e"), "Triceps" },
                    { new Guid("6e09dc32-23d1-4a82-aaeb-ba47c852ebee"), "Forearms" },
                    { new Guid("b0de268a-543f-447c-ba5a-21fb35e19146"), "Back" },
                    { new Guid("92ee6e17-569e-4e39-9f23-af028206431a"), "Cardio" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Categories_CategoryId",
                table: "Exercises",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
