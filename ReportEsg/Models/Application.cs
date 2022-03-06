using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Titolo")]
        public string Title { get; set; }
        [Display(Name = "Descrizione")]
        public string Description { get; set; }

        public bool NeedsScoreCalculations { get; set; }

        public OrganizationCategoryPerApplication OrganizationCategoryPerApplication;

        public List<Theme> Themes { get; set; }

        public List<ApplicationSurvey> ApplicationSurveys { get; set; }

    }
}
