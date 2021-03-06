// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReportEsg.Data;

namespace ReportEsg.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220220110443_AddApplicationSurveys")]
    partial class AddApplicationSurveys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ReportEsg.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurvey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<int>("ThemeId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ThemeId");

                    b.ToTable("ApplicationSurveys");
                });

            modelBuilder.Entity("ReportEsg.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("PillarId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("PillarId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrganizationCategories");
                });

            modelBuilder.Entity("ReportEsg.Models.Pillar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Pillars");
                });

            modelBuilder.Entity("ReportEsg.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ReportEsg.Models.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<int>("AreaId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AreaId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("ReportEsg.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Username");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("ReportEsg.Models.AdminUser", b =>
                {
                    b.HasBaseType("ReportEsg.Models.User");

                    b.HasDiscriminator().HasValue("AdminUser");
                });

            modelBuilder.Entity("ReportEsg.Models.ConsultantUser", b =>
                {
                    b.HasBaseType("ReportEsg.Models.User");

                    b.Property<string>("FirstName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasDiscriminator().HasValue("ConsultantUser");
                });

            modelBuilder.Entity("ReportEsg.Models.Organization", b =>
                {
                    b.HasBaseType("ReportEsg.Models.User");

                    b.Property<string>("Cap")
                        .HasColumnType("text");

                    b.Property<int>("CompanyCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("CompanyName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Indirizzo")
                        .HasColumnType("text");

                    b.Property<string>("Località")
                        .HasColumnType("text");

                    b.Property<int?>("OrganizationCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("PartitaIva")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Provincia")
                        .HasColumnType("text");

                    b.HasIndex("OrganizationCategoryId");

                    b.HasDiscriminator().HasValue("Organization");
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurvey", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.Area", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.Pillar", "Pillar")
                        .WithMany()
                        .HasForeignKey("PillarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.Pillar", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.Theme", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.User", b =>
                {
                    b.HasOne("ReportEsg.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.Organization", b =>
                {
                    b.HasOne("ReportEsg.Models.OrganizationCategory", "OrganizationCategory")
                        .WithMany()
                        .HasForeignKey("OrganizationCategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
