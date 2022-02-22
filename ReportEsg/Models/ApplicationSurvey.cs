using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class ApplicationSurvey : Survey
    {

        public int ApplicationId { get; set; }
        [Display(Name = "Applicazione")]
        public Application Application { get; set; }

        [Display(Name = "Seleziona il Tema materiale")]
        public int ThemeId { get; set; }
        [Display(Name = "Tema materiale")]
        public Theme Theme { get; set; }
    }
}
