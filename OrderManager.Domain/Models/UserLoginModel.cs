using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "É obrigatório o informe de seu email.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É obrigatório o informe de ua senha.")]
        public string Password { get; set; }
    }
}
