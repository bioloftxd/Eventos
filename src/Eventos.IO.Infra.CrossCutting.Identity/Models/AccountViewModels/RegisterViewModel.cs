﻿using System.ComponentModel.DataAnnotations;

namespace Eventos.IO.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é requerido.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é requerido.")]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O email é requerido.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
