using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int Score { get; set; }

        public int ApplicationSurveyQuestionId { get; set; }
        public ApplicationSurveyQuestion ApplicationSurveyQuestion { get; set; }
    }
}
