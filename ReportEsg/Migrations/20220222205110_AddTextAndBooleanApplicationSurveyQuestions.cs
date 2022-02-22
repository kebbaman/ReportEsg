using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class AddTextAndBooleanApplicationSurveyQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ApplicationSurveyQuestions",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ApplicationSurveyQuestions");
        }
    }
}
