using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineCoreApp.Migrations
{
    public partial class NowTheExerciseDetailShowTheSetNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetNumber",
                table: "ExerciseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetNumber",
                table: "ExerciseDetails");
        }
    }
}
