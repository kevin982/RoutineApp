using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineApp.Migrations
{
    public partial class WeAddedColumnsToUsersAndNowIRoutineIsAssignedToASinglePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutineUser");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Routines",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ExercisesDatils",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BeganToWorkOutOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routines_UserId",
                table: "Routines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesDatils_UserId",
                table: "ExercisesDatils",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesDatils_AspNetUsers_UserId",
                table: "ExercisesDatils",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_AspNetUsers_UserId",
                table: "Routines",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesDatils_AspNetUsers_UserId",
                table: "ExercisesDatils");

            migrationBuilder.DropForeignKey(
                name: "FK_Routines_AspNetUsers_UserId",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Routines_UserId",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_ExercisesDatils_UserId",
                table: "ExercisesDatils");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Routines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExercisesDatils");

            migrationBuilder.DropColumn(
                name: "BeganToWorkOutOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "RoutineUser",
                columns: table => new
                {
                    RoutinesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineUser", x => new { x.RoutinesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoutineUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutineUser_Routines_RoutinesId",
                        column: x => x.RoutinesId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutineUser_UsersId",
                table: "RoutineUser",
                column: "UsersId");
        }
    }
}
