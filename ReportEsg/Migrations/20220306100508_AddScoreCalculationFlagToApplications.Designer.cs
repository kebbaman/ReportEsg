﻿// <auto-generated />
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
    [Migration("20220306100508_AddScoreCalculationFlagToApplications")]
    partial class AddScoreCalculationFlagToApplications
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

                    b.Property<bool>("NeedsScoreCalculations")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSession", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<string>("ChoosenThemes")
                        .HasColumnType("text");

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<string>("CompletedSurveys")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Username")
                        .HasColumnType("character varying(50)");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Username");

                    b.ToTable("ApplicationSessions");
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurveyResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationSessionId")
                        .HasColumnType("integer");

                    b.Property<int>("ApplicationSurveyId")
                        .HasColumnType("integer");

                    b.Property<string>("SurveyResultJson")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationSessionId");

                    b.HasIndex("ApplicationSurveyId");

                    b.ToTable("ApplicationSurveyResults");
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

            modelBuilder.Entity("ReportEsg.Models.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationSurveyQuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationSurveyQuestionId");

                    b.ToTable("Choices");
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

            modelBuilder.Entity("ReportEsg.Models.OrganizationCategoryPerApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<int>("OrganizationCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("OrganizationCategoryId");

                    b.ToTable("OrganizationCategoryPerApplication");
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationDetailsSurveySession", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrganizationDetailsSurveyId")
                        .HasColumnType("integer");

                    b.Property<string>("SurveyResult")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("character varying(50)");

                    b.HasKey("ID");

                    b.HasIndex("OrganizationDetailsSurveyId");

                    b.HasIndex("Username");

                    b.ToTable("OrganizationDetailsSurveySessions");
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

            modelBuilder.Entity("ReportEsg.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Surveys");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Survey");
                });

            modelBuilder.Entity("ReportEsg.Models.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SurveyId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SurveyQuestion");
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

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurvey", b =>
                {
                    b.HasBaseType("ReportEsg.Models.Survey");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer");

                    b.Property<int>("ThemeId")
                        .HasColumnType("integer");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ThemeId");

                    b.HasDiscriminator().HasValue("ApplicationSurvey");
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationDetailsSurvey", b =>
                {
                    b.HasBaseType("ReportEsg.Models.Survey");

                    b.Property<int>("OrganizationCategoryId")
                        .HasColumnType("integer");

                    b.HasIndex("OrganizationCategoryId");

                    b.HasDiscriminator().HasValue("OrganizationDetailsSurvey");
                });

            modelBuilder.Entity("ReportEsg.Models.SimpleSurvey", b =>
                {
                    b.HasBaseType("ReportEsg.Models.Survey");

                    b.HasDiscriminator().HasValue("SimpleSurvey");
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurveyQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.SurveyQuestion");

                    b.Property<int>("ApplicationSurveyId")
                        .HasColumnType("integer");

                    b.Property<bool?>("HasOther")
                        .HasColumnType("boolean");

                    b.HasIndex("ApplicationSurveyId");

                    b.HasDiscriminator().HasValue("ApplicationSurveyQuestion");
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationDetailsSurveyQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.SurveyQuestion");

                    b.Property<int>("OrganizationDetailsSurveyId")
                        .HasColumnType("integer");

                    b.HasIndex("OrganizationDetailsSurveyId");

                    b.HasDiscriminator().HasValue("OrganizationDetailsSurveyQuestion");
                });

            modelBuilder.Entity("ReportEsg.Models.SimpleSurveyQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.SurveyQuestion");

                    b.Property<int>("SimpleSurveyId")
                        .HasColumnType("integer");

                    b.HasIndex("SimpleSurveyId");

                    b.HasDiscriminator().HasValue("SimpleSurveyQuestion");
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

                    b.Property<string>("CompanyName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Indirizzo")
                        .HasColumnType("text");

                    b.Property<string>("Località")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationCategoryId")
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

            modelBuilder.Entity("ReportEsg.Models.BooleanApplicationSurveyQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.ApplicationSurveyQuestion");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("BooleanApplicationSurveyQuestion");
                });

            modelBuilder.Entity("ReportEsg.Models.CheckboxApplicationSurveyQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.ApplicationSurveyQuestion");

                    b.HasDiscriminator().HasValue("CheckboxApplicationSurveyQuestion");
                });

            modelBuilder.Entity("ReportEsg.Models.RadioApplicationSurveyQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.ApplicationSurveyQuestion");

                    b.HasDiscriminator().HasValue("RadioApplicationSurveyQuestion");
                });

            modelBuilder.Entity("ReportEsg.Models.TextApplicationSurveyQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.ApplicationSurveyQuestion");

                    b.HasDiscriminator().HasValue("TextApplicationSurveyQuestion");
                });

            modelBuilder.Entity("ReportEsg.Models.TextOrganizationDetailsQuestion", b =>
                {
                    b.HasBaseType("ReportEsg.Models.OrganizationDetailsSurveyQuestion");

                    b.HasDiscriminator().HasValue("TextOrganizationDetailsQuestion");
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSession", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurveyResult", b =>
                {
                    b.HasOne("ReportEsg.Models.ApplicationSession", "ApplicationSession")
                        .WithMany("SurveyResults")
                        .HasForeignKey("ApplicationSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.ApplicationSurvey", "ApplicationSurvey")
                        .WithMany()
                        .HasForeignKey("ApplicationSurveyId")
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

            modelBuilder.Entity("ReportEsg.Models.Choice", b =>
                {
                    b.HasOne("ReportEsg.Models.ApplicationSurveyQuestion", "ApplicationSurveyQuestion")
                        .WithMany("Choices")
                        .HasForeignKey("ApplicationSurveyQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationCategoryPerApplication", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.OrganizationCategory", "OrganizationCategory")
                        .WithMany()
                        .HasForeignKey("OrganizationCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationDetailsSurveySession", b =>
                {
                    b.HasOne("ReportEsg.Models.OrganizationDetailsSurvey", "OrganizationDetailsSurvey")
                        .WithMany()
                        .HasForeignKey("OrganizationDetailsSurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("ReportEsg.Models.Pillar", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.SurveyQuestion", b =>
                {
                    b.HasOne("ReportEsg.Models.Survey", null)
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("ReportEsg.Models.Theme", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany("Themes")
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

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurvey", b =>
                {
                    b.HasOne("ReportEsg.Models.Application", "Application")
                        .WithMany("ApplicationSurveys")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportEsg.Models.Theme", "Theme")
                        .WithMany("ApplicationSurveys")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationDetailsSurvey", b =>
                {
                    b.HasOne("ReportEsg.Models.OrganizationCategory", "OrganizationCategory")
                        .WithMany()
                        .HasForeignKey("OrganizationCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.ApplicationSurveyQuestion", b =>
                {
                    b.HasOne("ReportEsg.Models.ApplicationSurvey", "ApplicationSurvey")
                        .WithMany("ApplicationSurveyQuestions")
                        .HasForeignKey("ApplicationSurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.OrganizationDetailsSurveyQuestion", b =>
                {
                    b.HasOne("ReportEsg.Models.OrganizationDetailsSurvey", "OrganizationDetailsSurvey")
                        .WithMany("OrganizationDetailsSurveyQuestions")
                        .HasForeignKey("OrganizationDetailsSurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.SimpleSurveyQuestion", b =>
                {
                    b.HasOne("ReportEsg.Models.SimpleSurvey", "SimpleSurvey")
                        .WithMany()
                        .HasForeignKey("SimpleSurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportEsg.Models.Organization", b =>
                {
                    b.HasOne("ReportEsg.Models.OrganizationCategory", "OrganizationCategory")
                        .WithMany()
                        .HasForeignKey("OrganizationCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
