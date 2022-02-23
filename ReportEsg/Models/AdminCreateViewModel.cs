using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class AdminCreateViewModel
    {
        public string Username { get => this.Email; }
        [Key]
        [Required(ErrorMessage = "L'indirizzo email è obbligatorio!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Inserire un indirizzo email valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "È necessario inserire una password")]
        [StringLength(200, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
