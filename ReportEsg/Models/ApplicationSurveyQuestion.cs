using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class ApplicationSurveyQuestion : SurveyQuestion
    {
        [Display(Name = "Questionario")]
        public int ApplicationSurveyId { get; set; }
        public ApplicationSurvey ApplicationSurvey { get; set; }

        //public virtual List<Choice> Choices { get; set; }
        public List<Choice> Choices { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "choices")]
        public List<string> ChoicesList
        {
            get
            {
                if (Choices is null)
                    return null;
                else
                {
                    List<string> list = new List<string>();
                    Choices.ForEach(x => list.Add(x.Description));
                    return list;
                }
                
            }
        }

        [JsonProperty(PropertyName = "hasOther")]
        [Display(Name = "Includere campo altro?")]
        public virtual bool? HasOther { get; set; }

    }
}
