using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class AnswerWithScores
    {
        [Display(Name = "Codice")]
        public string Tema { get; set; }

        [Display(Name = "Codice")]
        public string CodiceDomanda { get; set; }
        [Display(Name = "Domanda")]
        public string Domanda { get; set; }
        [Display(Name = "Risposta")]
        public string Risposta { get; set; }
        [Display(Name = "Punteggio")]
        public decimal Score { get; set; }

        public AnswerWithScores(string tema, string codiceDomanda, string domanda, string risposta, decimal score)
        {
            Tema = tema;
            CodiceDomanda = codiceDomanda;
            Domanda = domanda;
            Risposta = risposta;
            Score = score;
        }
    }
}
