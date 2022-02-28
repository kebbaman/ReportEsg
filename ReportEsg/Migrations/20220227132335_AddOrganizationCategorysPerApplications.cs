using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReportEsg.Migrations
{
    public partial class AddOrganizationCategorysPerApplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_OrganizationCategories_OrganizationCategoryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyCategoryId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "OrganizationCategoryPerApplication",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    OrganizationCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCategoryPerApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationCategoryPerApplication_Applications_Application~",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationCategoryPerApplication_OrganizationCategories_O~",
                        column: x => x.OrganizationCategoryId,
                        principalTable: "OrganizationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCategoryPerApplication_ApplicationId",
                table: "OrganizationCategoryPerApplication",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCategoryPerApplication_OrganizationCategoryId",
                table: "OrganizationCategoryPerApplication",
                column: "OrganizationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OrganizationCategories_OrganizationCategoryId",
                table: "Users",
                column: "OrganizationCategoryId",
                principalTable: "OrganizationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_OrganizationCategories_OrganizationCategoryId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "OrganizationCategoryPerApplication");

            migrationBuilder.AddColumn<int>(
                name: "CompanyCategoryId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OrganizationCategories_OrganizationCategoryId",
                table: "Users",
                column: "OrganizationCategoryId",
                principalTable: "OrganizationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
