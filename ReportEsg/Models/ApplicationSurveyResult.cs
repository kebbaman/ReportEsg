using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class ApplicationSurveyResult
    {
        [Key]
        public int Id { get; set; }

        public string SurveyResultJson { get; set; }

        [NotMapped]
        public Dictionary<string, object> SurveyResultObject
        {
            get => JsonConvert.DeserializeObject<Dictionary<string, object>>(this.SurveyResultJson.ToString());
        }

        public int ApplicationSurveyId { get; set; }
        public ApplicationSurvey ApplicationSurvey { get; set; }


        public int ApplicationSessionId { get; set; }
        public ApplicationSession ApplicationSession { get; set; }

    }
}
