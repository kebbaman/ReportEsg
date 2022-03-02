using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReportEsg.Migrations
{
    public partial class AddRadioAndCheckboxesQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasOther",
                table: "SurveyQuestions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    ApplicationSurveyQuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choices_SurveyQuestions_ApplicationSurveyQuestionId",
                        column: x => x.ApplicationSurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_ApplicationSurveyQuestionId",
                table: "Choices",
                column: "ApplicationSurveyQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropColumn(
                name: "HasOther",
                table: "SurveyQuestions");
        }
    }
}
