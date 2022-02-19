using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }

        //[JsonProperty(PropertyName = "title")]
        //[Required(ErrorMessage = "È obbligatorio inserire un titolo!")]
        [StringLength(255, ErrorMessage = "Il titolo può essere lungo massimo 255 caratteri.")]
        [Display(Name = "Titolo Questionario")]
        public string Title { get; set; }

        //[JsonProperty(PropertyName = "questions")]
        //public List<SurveyQuestion> Questions { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

    }
}
