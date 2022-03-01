using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class EditedSurveyResultsInApplicationSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurveyResults",
                table: "ApplicationSessions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "SurveyResults",
                table: "ApplicationSessions",
                type: "text[]",
                nullable: true);
        }
    }
}
