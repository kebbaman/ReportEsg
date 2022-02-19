using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class OrganizationCategory
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Descrizione")]
        public string Description { get; set; }
    }
}
