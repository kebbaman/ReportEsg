using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class ApplicationSession
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        [ForeignKey("Organization")]
        public string Username { get; set; }
        public Organization Organization { get; set; }

       
    }
}
