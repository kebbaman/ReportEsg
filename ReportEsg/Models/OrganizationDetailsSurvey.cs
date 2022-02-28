using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class OrganizationDetailsSurvey : Survey
    {
        [Display(Name = "Categoria di organizzazioni a cui è destinato il questionario")]
        public int OrganizationCategoryId { get; set; }
        [Display(Name = "Categoria di organizzazioni")]
        public OrganizationCategory OrganizationCategory { get; set; }

        [JsonProperty(PropertyName = "questions")]
        public List<OrganizationDetailsSurveyQuestion> OrganizationDetailsSurveyQuestions { get; set; }

    }
}
