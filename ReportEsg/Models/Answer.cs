using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class Answer
    {
        [Display(Name = "Codice")]
        public string CodiceDomanda { get; set; }
        [Display(Name = "Domanda")]
        public string Domanda { get; set; }
        [Display(Name = "Risposta")]
        public string Risposta { get; set; }

        public Answer(string codiceDomanda, string domanda, string risposta)
        {
            CodiceDomanda = codiceDomanda;
            Domanda = domanda;
            Risposta = risposta;
        }
    }
}
