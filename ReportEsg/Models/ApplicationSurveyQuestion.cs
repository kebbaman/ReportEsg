using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class ApplicationSurveyQuestion : SurveyQuestion
    {
        [Display(Name = "Questionario")]
        public int ApplicationSurveyId { get; set; }
        public ApplicationSurvey ApplicationSurvey { get; set; }

    }
}
