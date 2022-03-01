using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class AddCompletedSurveysToApplicationSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedSurveys",
                table: "ApplicationSessions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedSurveys",
                table: "ApplicationSessions");
        }
    }
}
