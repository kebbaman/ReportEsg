using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Lo Username è obbligatorio!")]
        [StringLength(50, ErrorMessage = "Lo username può essere lungo massimo 50 caratteri.")]
        public string Username { get; set; }  //Come username utilizzo l'indirizzo email.

        [Required(ErrorMessage = "L'indirizzo email è obbligatorio!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "L'indirizzo email è obbligatorio!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria!")]
        [StringLength(200)]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        public string PasswordSalt { get; set; }

        public int RoleId { get; set; }

        [Display(Name = "Ruolo")]
        public Role Role { get; set; }

    }

    public class UserLoginModel
    {
        [Required(ErrorMessage = "Lo Username è obbligatorio!")]
        [StringLength(50, ErrorMessage = "Lo username può essere lungo massimo 50 caratteri.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "È necessario inserire una password")]
        [StringLength(200, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
