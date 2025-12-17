using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Migrations.EducationDb
{
    /// <inheritdoc />
    public partial class CreateCourseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Tests_TestId",
                table: "TestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Tests_TestId1",
                table: "TestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgresses_Lessons_LessonId1",
                table: "UserProgresses");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_UserProgresses_LessonId1",
                table: "UserProgresses");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_TestId1",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "LessonId1",
                table: "UserProgresses");

            migrationBuilder.DropColumn(
                name: "TestId1",
                table: "TestResults");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Tests_TestId",
                table: "TestResults",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Tests_TestId",
                table: "TestResults");

            migrationBuilder.AddColumn<int>(
                name: "LessonId1",
                table: "UserProgresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId1",
                table: "TestResults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    UnlockedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_LessonId1",
                table: "UserProgresses",
                column: "LessonId1");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestId1",
                table: "TestResults",
                column: "TestId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Tests_TestId",
                table: "TestResults",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Tests_TestId1",
                table: "TestResults",
                column: "TestId1",
                principalTable: "Tests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgresses_Lessons_LessonId1",
                table: "UserProgresses",
                column: "LessonId1",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
