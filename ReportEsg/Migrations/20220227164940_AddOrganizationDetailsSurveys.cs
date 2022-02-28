using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class AddOrganizationDetailsSurveys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationDetailsSurveyId",
                table: "SurveyQuestions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_OrganizationDetailsSurveyId",
                table: "SurveyQuestions",
                column: "OrganizationDetailsSurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_OrganizationDetailsSurveyId",
                table: "SurveyQuestions",
                column: "OrganizationDetailsSurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_OrganizationDetailsSurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_OrganizationDetailsSurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "OrganizationDetailsSurveyId",
                table: "SurveyQuestions");
        }
    }
}
