using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Pillar
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Descrizione")]
        public string Description { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

    }
}
