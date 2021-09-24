using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutineMS_API.Migrations
{
    public partial class NowEachDayCanHaveMultipleRoutines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Routines_RoutineId",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Days_RoutineId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "RoutineId",
                table: "Days");

            migrationBuilder.CreateTable(
                name: "DayRoutine",
                columns: table => new
                {
                    DaysId = table.Column<int>(type: "int", nullable: false),
                    RoutinesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayRoutine", x => new { x.DaysId, x.RoutinesId });
                    table.ForeignKey(
                        name: "FK_DayRoutine_Days_DaysId",
                        column: x => x.DaysId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayRoutine_Routines_RoutinesId",
                        column: x => x.RoutinesId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayRoutine_RoutinesId",
                table: "DayRoutine",
                column: "RoutinesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayRoutine");

            migrationBuilder.AddColumn<Guid>(
                name: "RoutineId",
                table: "Days",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Days_RoutineId",
                table: "Days",
                column: "RoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Routines_RoutineId",
                table: "Days",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
