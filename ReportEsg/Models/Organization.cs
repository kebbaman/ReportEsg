using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Organization : User
    {
        [StringLength(100)]
        [Display(Name = "Ragione sociale")]
        public string CompanyName { get; set; }
        [Display(Name = "Telefono")]
        public string PhoneNumber { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Località { get; set; }
        public string Provincia { get; set; }
        [Display(Name = "Partita iva")]
        public string PartitaIva { get; set; }


        public int CompanyCategoryId { get; set; }

        [Display(Name = "Categoria")]
        public OrganizationCategory OrganizationCategory { get; set; }

        //public ICollection<PreAssessmentSurvey> PreAssessmentSurveys { get; set; }
    }
}
