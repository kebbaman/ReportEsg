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
        //Utilizzate dependency injection e appsetting json per evitare l'hardcoding della connection string
        /*

        public DbSet<SurveyType> SurveyTypes { get; set; }
        public DbSet<SurveyModel> SurveyModels { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        
        public DbSet<PreAssessmentSurvey> PreAssessmentSurveys { get; set; }
        public DbSet<CompanyDetailsSurvey> CompanyDetailsSurveys { get; set; }

        public DbSet<SurveyModel_CompanyCategory> SurveyModel_CompanyCategories { get; set; }
        public DbSet<PreAssessmentSurvey_CompanyCategory> PreAssessmentSurvey_CompanyCategories { get; set; }
        public DbSet<CompanyDetailsSurvey_CompanyCategory> CompanyDetailsSurvey_CompanyCategories { get; set; }
        public DbSet<PiattaformaReportingEsgPmi.Models.GenericSurvey> GenericSurvey { get; set; }
        */


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
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        

        



    }

}
