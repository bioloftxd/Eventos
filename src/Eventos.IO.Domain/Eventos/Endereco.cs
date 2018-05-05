using Eventos.IO.Domain.Core.Models;
using FluentValidation;
using System;

namespace Eventos.IO.Domain.Eventos
{
    public class Endereco : Entity<Endereco>
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro{ get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? EventoId { get; private set; }
        
        //EF Propriedade de Navegação
        public virtual Evento Evento { get; private set; }


        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid eventoId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            EventoId = eventoId;
        }

        protected Endereco(){}

        public override bool ehValido()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("O logradouro precisa ser fornecido!")
                .Length(2, 150).WithMessage("O logradouro precisa conter entre 2 e 150 caracteres!");

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O bairro precisa ser fornecido!")
                .Length(2, 150).WithMessage("O bairro precisa conter entre 2 e 150 caracteres!");

            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("O CEP precisa ser fornecido!")
                .Length(8).WithMessage("O CEP precisa ter 8 caracteres!");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("A cidade precisa ser fornecida!")
                .Length(2, 150).WithMessage("A cidade precisa ter entre 2 e 150 caracteres!");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O estado precisa ser fornecido!")
                .Length(2, 150).WithMessage("O estado precisa ter entre 2 e 150 caracteres!");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O numero precis ser fornecido!")
                .Length(1, 10).WithMessage("O numero precisa ter entre 1 e 10 caracteres!");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

    }
}