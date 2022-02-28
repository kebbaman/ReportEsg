using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReportEsg.Migrations
{
    public partial class AddOrganizationDetailsSurvveySessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationDetailsSurveySessions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    SurveyResult = table.Column<string>(nullable: true),
                    OrganizationDetailsSurveyId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationDetailsSurveySessions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrganizationDetailsSurveySessions_Surveys_OrganizationDetai~",
                        column: x => x.OrganizationDetailsSurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationDetailsSurveySessions_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationDetailsSurveySessions_OrganizationDetailsSurvey~",
                table: "OrganizationDetailsSurveySessions",
                column: "OrganizationDetailsSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationDetailsSurveySessions_Username",
                table: "OrganizationDetailsSurveySessions",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationDetailsSurveySessions");
        }
    }
}
