using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReportEsg.Migrations
{
    public partial class AddUsersEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    PasswordSalt = table.Column<string>(maxLength: 200, nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Indirizzo = table.Column<string>(nullable: true),
                    Cap = table.Column<string>(nullable: true),
                    Località = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true),
                    PartitaIva = table.Column<string>(nullable: true),
                    CompanyCategoryId = table.Column<int>(nullable: true),
                    OrganizationCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Users_OrganizationCategories_OrganizationCategoryId",
                        column: x => x.OrganizationCategoryId,
                        principalTable: "OrganizationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationCategoryId",
                table: "Users",
                column: "OrganizationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OrganizationCategories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
