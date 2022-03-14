using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class OrganizationDetailsSurveySession
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        [Display(Name = "Risultato in formato JSON")]
        public string SurveyResult { get; set; }
        public int OrganizationDetailsSurveyId { get; set; }
        [Display(Name = "Questionario")]
        public OrganizationDetailsSurvey OrganizationDetailsSurvey { get; set; }

        [ForeignKey("Organization")]
        public string Username { get; set; }
        [Display(Name = "Organizzazione")]
        public Organization Organization { get; set; }

        [NotMapped]
        public Dictionary<string, object> SurveyResultObject { get => JsonConvert.DeserializeObject<Dictionary<string, object>>(this.SurveyResult.ToString()); }
    }
}
