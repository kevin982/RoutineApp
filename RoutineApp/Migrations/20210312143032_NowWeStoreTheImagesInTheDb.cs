using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineApp.Migrations
{
    public partial class NowWeStoreTheImagesInTheDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseDetails_AspNetUsers_UserId",
                table: "ExerciseDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseDetails_UserId",
                table: "ExerciseDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExerciseDetails");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Images",
                newName: "Img");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Img",
                table: "Images",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ExerciseDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDetails_UserId",
                table: "ExerciseDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseDetails_AspNetUsers_UserId",
                table: "ExerciseDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
