using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class SimpleSurveyQuestion : SurveyQuestion
    {
        [Display(Name = "Questionario")]
        public int SimpleSurveyId { get; set; }
        public SimpleSurvey SimpleSurvey { get; set; }
    }
}
