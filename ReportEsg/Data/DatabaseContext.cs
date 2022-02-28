using Microsoft.EntityFrameworkCore;
using ReportEsg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Data
{
    public class DatabaseContext : DbContext
    {
        //Utilizzati dependency injection e appsetting json per evitare l'hardcoding della connection string

        public DbSet<Role> Roles { get; set; }
        public DbSet<OrganizationCategory> OrganizationCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<ConsultantUser> ConsultantUsers { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Pillar> Pillars { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Theme> Themes { get; set; }

        public DbSet<ApplicationSurvey> ApplicationSurveys { get; set; }
        public DbSet<ApplicationSurveyQuestion> ApplicationSurveyQuestions { get; set; }
        public DbSet<TextApplicationSurveyQuestion> TextApplicationSurveyQuestions { get; set; }
        public DbSet<BooleanApplicationSurveyQuestion> BooleanApplicationSurveyQuestions { get; set; }

        public DbSet<TextApplicationSurveyQuestion> TextApplicationSurveyQuestion { get; set; }

        public DbSet<OrganizationCategoryPerApplication> OrganizationCategoryPerApplication { get; set; }

        public DbSet<SimpleSurvey> SimpleSurveys { get; set; }

        public DbSet<SimpleSurveyQuestion> SimpleSurveyQuestions { get; set; }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }

        public DbSet<OrganizationDetailsSurvey> OrganizationDetailsSurveys { get; set; }

        public DbSet<OrganizationDetailsSurveyQuestion> OrganizationDetailsSurveyQuestions { get; set; }
        public DbSet<TextOrganizationDetailsQuestion> TextOrganizationDetailsQuestion { get; set; }
        public DbSet<OrganizationDetailsSurveySession> OrganizationDetailsSurveySessions  { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        


    }

}
