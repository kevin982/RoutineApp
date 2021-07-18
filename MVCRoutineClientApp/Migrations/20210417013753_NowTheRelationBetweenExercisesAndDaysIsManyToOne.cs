using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineCoreApp.Migrations
{
    public partial class NowTheRelationBetweenExercisesAndDaysIsManyToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayExercise");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "Days",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Days_ExerciseId",
                table: "Days",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Exercises_ExerciseId",
                table: "Days",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Exercises_ExerciseId",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Days_ExerciseId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Days");

            migrationBuilder.CreateTable(
                name: "DayExercise",
                columns: table => new
                {
                    DaysToTrainId = table.Column<int>(type: "int", nullable: false),
                    ExercisesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayExercise", x => new { x.DaysToTrainId, x.ExercisesId });
                    table.ForeignKey(
                        name: "FK_DayExercise_Days_DaysToTrainId",
                        column: x => x.DaysToTrainId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayExercise_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayExercise_ExercisesId",
                table: "DayExercise",
                column: "ExercisesId");
        }
    }
}
