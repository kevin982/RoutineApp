using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineApp.Migrations
{
    public partial class WeightEntityHasBeenAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesDatils_AspNetUsers_UserId",
                table: "ExercisesDatils");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesDatils_Exercises_ExerciseId",
                table: "ExercisesDatils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExercisesDatils",
                table: "ExercisesDatils");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ExercisesDatils",
                newName: "ExerciseDetails");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisesDatils_UserId",
                table: "ExerciseDetails",
                newName: "IX_ExerciseDetails_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisesDatils_ExerciseId",
                table: "ExerciseDetails",
                newName: "IX_ExerciseDetails_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseDetails",
                table: "ExerciseDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExactWeight = table.Column<float>(type: "real", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weights_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weights_UserId1",
                table: "Weights",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseDetails_AspNetUsers_UserId",
                table: "ExerciseDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseDetails_Exercises_ExerciseId",
                table: "ExerciseDetails",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseDetails_AspNetUsers_UserId",
                table: "ExerciseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseDetails_Exercises_ExerciseId",
                table: "ExerciseDetails");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseDetails",
                table: "ExerciseDetails");

            migrationBuilder.RenameTable(
                name: "ExerciseDetails",
                newName: "ExercisesDatils");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseDetails_UserId",
                table: "ExercisesDatils",
                newName: "IX_ExercisesDatils_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseDetails_ExerciseId",
                table: "ExercisesDatils",
                newName: "IX_ExercisesDatils_ExerciseId");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExercisesDatils",
                table: "ExercisesDatils",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesDatils_AspNetUsers_UserId",
                table: "ExercisesDatils",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesDatils_Exercises_ExerciseId",
                table: "ExercisesDatils",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
