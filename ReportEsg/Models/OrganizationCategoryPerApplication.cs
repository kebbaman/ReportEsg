using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class OrganizationCategoryPerApplication
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Applicazione")]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        [Display(Name = "Categoria organizzazione")]
        public int OrganizationCategoryId { get; set; }
        [Display(Name = "Categoria organizzazione")]
        public OrganizationCategory OrganizationCategory { get; set; }
    }
}
