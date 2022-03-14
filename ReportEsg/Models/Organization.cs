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


        public int OrganizationCategoryId { get; set; }

        [Display(Name = "Categoria")]
        public OrganizationCategory OrganizationCategory { get; set; }

        //public ICollection<PreAssessmentSurvey> PreAssessmentSurveys { get; set; }
    }

    public class CompanyViewModel
    {
        public string Username { get => this.Email; }
        [Key]
        [Required(ErrorMessage = "L'indirizzo email è obbligatorio!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Inserire un indirizzo email valido.")]
        public string Email { get; set; }

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
        [Display(Name = "Categoria")]
        public int OrganizationCategoryId { get; set; }
    }
}
