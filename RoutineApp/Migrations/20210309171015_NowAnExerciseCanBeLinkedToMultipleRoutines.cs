using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineApp.Migrations
{
    public partial class NowAnExerciseCanBeLinkedToMultipleRoutines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Routines_RoutineId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_RoutineId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "RoutineId",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "ExerciseRoutine",
                columns: table => new
                {
                    ExercisesId = table.Column<int>(type: "int", nullable: false),
                    RoutinesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseRoutine", x => new { x.ExercisesId, x.RoutinesId });
                    table.ForeignKey(
                        name: "FK_ExerciseRoutine_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseRoutine_Routines_RoutinesId",
                        column: x => x.RoutinesId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseRoutine_RoutinesId",
                table: "ExerciseRoutine",
                column: "RoutinesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseRoutine");

            migrationBuilder.AddColumn<int>(
                name: "RoutineId",
                table: "Exercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_RoutineId",
                table: "Exercises",
                column: "RoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Routines_RoutineId",
                table: "Exercises",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
