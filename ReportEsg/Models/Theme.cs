using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Descrizione")]
        public string Description { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public List<ApplicationSurvey> ApplicationSurveys { get; set; }
    }
}
