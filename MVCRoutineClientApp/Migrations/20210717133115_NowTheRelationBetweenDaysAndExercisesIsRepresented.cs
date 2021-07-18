using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineCoreApp.Migrations
{
    public partial class NowTheRelationBetweenDaysAndExercisesIsRepresented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.CreateTable(
                name: "DayExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayExercises_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayExercises_DayId",
                table: "DayExercises",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_DayExercises_ExerciseId",
                table: "DayExercises",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayExercises");

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MailHtmlCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                });
        }
    }
}
