using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage ="É obrigatório o informe de seu nome.")]
        public string Name { get; private set; }
        

        [Required(ErrorMessage = "É obrigatório o informe de seu email.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; private set; }


        [Required(ErrorMessage = "É obrigatório o informe de uma senha.")]
        public string Password { get; private set; }


        [Required(ErrorMessage = "É obrigatório o informe de seu endereço.")]
        public string Address { get; private set; }
    }
}
