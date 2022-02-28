using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class AddSimpleSurveys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSurveyQuestions_ApplicationSurveys_ApplicationSu~",
                table: "ApplicationSurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSurveys_Applications_ApplicationId",
                table: "ApplicationSurveys");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSurveys_Themes_ThemeId",
                table: "ApplicationSurveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationSurveys",
                table: "ApplicationSurveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationSurveyQuestions",
                table: "ApplicationSurveyQuestions");

            migrationBuilder.RenameTable(
                name: "ApplicationSurveys",
                newName: "Surveys");

            migrationBuilder.RenameTable(
                name: "ApplicationSurveyQuestions",
                newName: "SurveyQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationSurveys_ThemeId",
                table: "Surveys",
                newName: "IX_Surveys_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationSurveys_ApplicationId",
                table: "Surveys",
                newName: "IX_Surveys_ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationSurveyQuestions_ApplicationSurveyId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_ApplicationSurveyId");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Surveys",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "Surveys",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Surveys",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationSurveyId",
                table: "SurveyQuestions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SimpleSurveyId",
                table: "SurveyQuestions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "SurveyQuestions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SimpleSurveyId",
                table: "SurveyQuestions",
                column: "SimpleSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_ApplicationSurveyId",
                table: "SurveyQuestions",
                column: "ApplicationSurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_SimpleSurveyId",
                table: "SurveyQuestions",
                column: "SimpleSurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Applications_ApplicationId",
                table: "Surveys",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Themes_ThemeId",
                table: "Surveys",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_ApplicationSurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_SimpleSurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Applications_ApplicationId",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Themes_ThemeId",
                table: "Surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_SimpleSurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SimpleSurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.RenameTable(
                name: "Surveys",
                newName: "ApplicationSurveys");

            migrationBuilder.RenameTable(
                name: "SurveyQuestions",
                newName: "ApplicationSurveyQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_ThemeId",
                table: "ApplicationSurveys",
                newName: "IX_ApplicationSurveys_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_ApplicationId",
                table: "ApplicationSurveys",
                newName: "IX_ApplicationSurveys_ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_ApplicationSurveyId",
                table: "ApplicationSurveyQuestions",
                newName: "IX_ApplicationSurveyQuestions_ApplicationSurveyId");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "ApplicationSurveys",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "ApplicationSurveys",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationSurveyId",
                table: "ApplicationSurveyQuestions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationSurveys",
                table: "ApplicationSurveys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationSurveyQuestions",
                table: "ApplicationSurveyQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSurveyQuestions_ApplicationSurveys_ApplicationSu~",
                table: "ApplicationSurveyQuestions",
                column: "ApplicationSurveyId",
                principalTable: "ApplicationSurveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSurveys_Applications_ApplicationId",
                table: "ApplicationSurveys",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSurveys_Themes_ThemeId",
                table: "ApplicationSurveys",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
