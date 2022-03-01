using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class ApplicationSession
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Data ultima attività")]
        public DateTime DateTime { get; set; }

        public int ApplicationId { get; set; }
        [Display(Name = "Applicazione")]
        public Application Application { get; set; }

        [ForeignKey("Organization")]
        public string Username { get; set; }
        public Organization Organization { get; set; }
        [Display(Name = "Lista temi")]
        public string ChoosenThemes { get; set; }

        public bool Completed { get; set; }

        public string CompletedSurveys { get; set; }

        public List<ApplicationSurveyResult> SurveyResults { get; set; }


    }
}