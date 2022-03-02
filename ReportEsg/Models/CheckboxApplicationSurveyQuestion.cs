using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class CheckboxApplicationSurveyQuestion : ApplicationSurveyQuestion
    {
        [JsonProperty(PropertyName = "choices")]
        public override List<Choice> Choices { get; set; }
        
        [JsonProperty(PropertyName = "hasOther")]
        [Display(Name = "Includere campo altro?")]
        public override bool? HasOther { get; set; }


    }
}
