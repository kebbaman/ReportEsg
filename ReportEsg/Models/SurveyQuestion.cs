using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        [Display(Name = "Codice")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il titolo è obbligatorio!")]
        [JsonProperty(PropertyName = "title")]
        [Display(Name = "Domanda")]
        public string Title { get; set; }

        [Required]
        [JsonProperty(PropertyName = "type")]
        [Display(Name = "Tipologia")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "isRequired")]
        [Display(Name = "Risposta obbligatoria")]
        public bool IsRequired { get; set; }

    }
}
