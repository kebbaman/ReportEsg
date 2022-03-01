using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReportEsg.Migrations
{
    public partial class AddChoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    ApplicationSurveyQuestionId = table.Column<int>(nullable: false),
                    RadioApplicationSurveyQuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choice_SurveyQuestions_ApplicationSurveyQuestionId",
                        column: x => x.ApplicationSurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Choice_SurveyQuestions_RadioApplicationSurveyQuestionId",
                        column: x => x.RadioApplicationSurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choice_ApplicationSurveyQuestionId",
                table: "Choice",
                column: "ApplicationSurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Choice_RadioApplicationSurveyQuestionId",
                table: "Choice",
                column: "RadioApplicationSurveyQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choice");
        }
    }
}
