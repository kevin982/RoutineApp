using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineCoreApp.Migrations
{
    public partial class NowEachExerciseCanOnlyHaveAnImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseDetails");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseSetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    DayDone = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    SetNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSetDetails_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWeights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Kilos = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWeights_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSetDetails_ExerciseId",
                table: "ExerciseSetDetails",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeights_UserId",
                table: "UserWeights",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSetDetails");

            migrationBuilder.DropTable(
                name: "UserWeights");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "ExerciseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayDone = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    SetNumber = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseDetails_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kilos = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weights_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDetails_ExerciseId",
                table: "ExerciseDetails",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ExerciseId",
                table: "Images",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_UserId",
                table: "Weights",
                column: "UserId");
        }
    }
}
