using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class BooleanApplicationSurveyQuestion : ApplicationSurveyQuestion
    {
        [Display(Name = "Punteggio in caso di risposta affermativa")]
        public decimal Score { get; set; }
    }
}
