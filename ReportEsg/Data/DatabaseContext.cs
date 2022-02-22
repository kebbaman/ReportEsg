﻿using Microsoft.EntityFrameworkCore;
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



        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }





        public DbSet<ReportEsg.Models.TextApplicationSurveyQuestion> TextApplicationSurveyQuestion { get; set; }

    }

}
