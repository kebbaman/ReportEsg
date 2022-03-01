using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReportEsg.Migrations
{
    public partial class AddApplicationSurveyResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "SurveyResults",
                table: "ApplicationSessions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationSurveyResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyResultJson = table.Column<string>(nullable: true),
                    ApplicationSurveyId = table.Column<int>(nullable: false),
                    ApplicationSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSurveyResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationSurveyResults_ApplicationSessions_ApplicationSes~",
                        column: x => x.ApplicationSessionId,
                        principalTable: "ApplicationSessions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationSurveyResults_Surveys_ApplicationSurveyId",
                        column: x => x.ApplicationSurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSurveyResults_ApplicationSessionId",
                table: "ApplicationSurveyResults",
                column: "ApplicationSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSurveyResults_ApplicationSurveyId",
                table: "ApplicationSurveyResults",
                column: "ApplicationSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSurveyResults");

            migrationBuilder.DropColumn(
                name: "SurveyResults",
                table: "ApplicationSessions");
        }
    }
}
