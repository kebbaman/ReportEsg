using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class RadioApplicationSurveyQuestion : ApplicationSurveyQuestion
    {
        public List<Choice> Choices { get; set; }
    }
}
