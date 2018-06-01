using System;
using System.ComponentModel.DataAnnotations;

namespace Eventos.IO.Application.ViewModels
{
    public class OrganizadorViewModel
    {
        [Key]
        public Guid Id{ get; set; }

        [Required(ErrorMessage = "O nome é requerido.")]
        public string Nome{ get; set; }

        [Required(ErrorMessage = "O CPF é requerido.")]
        [StringLength(11)]
        public string CPF{ get; set; }

        [Required(ErrorMessage = "O email é requerido.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        public string Email{ get; set; }
    }
}