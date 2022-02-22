using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class AddApplicationSurveyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationSurveyId",
                table: "ApplicationSurveyQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSurveyQuestions_ApplicationSurveyId",
                table: "ApplicationSurveyQuestions",
                column: "ApplicationSurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSurveyQuestions_ApplicationSurveys_ApplicationSu~",
                table: "ApplicationSurveyQuestions",
                column: "ApplicationSurveyId",
                principalTable: "ApplicationSurveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSurveyQuestions_ApplicationSurveys_ApplicationSu~",
                table: "ApplicationSurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationSurveyQuestions_ApplicationSurveyId",
                table: "ApplicationSurveyQuestions");

            migrationBuilder.DropColumn(
                name: "ApplicationSurveyId",
                table: "ApplicationSurveyQuestions");
        }
    }
}
