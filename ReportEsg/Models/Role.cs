using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Inserire una descrizione!")]
        [Display(Name = "Descrizione")]
        public string Description { get; set; }
    }
}
