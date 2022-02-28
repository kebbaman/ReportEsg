using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class AddOrganizationCategoryIdToOrganizationDetailsSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationCategoryId",
                table: "Surveys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_OrganizationCategoryId",
                table: "Surveys",
                column: "OrganizationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_OrganizationCategories_OrganizationCategoryId",
                table: "Surveys",
                column: "OrganizationCategoryId",
                principalTable: "OrganizationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_OrganizationCategories_OrganizationCategoryId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_OrganizationCategoryId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "OrganizationCategoryId",
                table: "Surveys");
        }
    }
}
