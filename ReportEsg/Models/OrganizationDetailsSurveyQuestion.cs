using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class OrganizationDetailsSurveyQuestion: SurveyQuestion
    {
        [Display(Name = "Questionario")]
        public int OrganizationDetailsSurveyId { get; set; }
        public OrganizationDetailsSurvey OrganizationDetailsSurvey { get; set; }

    }
}
