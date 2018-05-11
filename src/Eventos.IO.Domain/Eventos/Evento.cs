using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.Organizadores;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos
{
    public class Evento : Entity<Evento>
    {

        public Evento(
            string nome, 
            DateTime dataInicio, 
            DateTime dataFim, 
            bool gratuito, 
            decimal valor, 
            bool online, 
            string nomeEmpresa)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
        }

        private Evento(){}

        public string Nome{ get; private set; }
        public string DescricaoCurta { get; private set; }
        public string DescricaoLonga { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Gratuito { get; private set; }
        public decimal Valor { get; private set; }
        public bool Online { get; private set; }
        public String NomeEmpresa { get; private set; }
        public bool Excluido { get; private set; }
        public ICollection<Tags> Tags { get; private set; }

        public Guid? CategoriaId { get; private set; }
        public Guid? EnderecoId { get; private set; }
        public Guid OrganizadorId { get; private set; }

        //EF propriedades de navegação
        public virtual Categoria Categoria { get; private set; }
        public virtual Endereco Endereco { get; private set; }
        public virtual Organizador Organizador { get; private set; }

        public void AtribuirEndereco(Endereco endereco)
        {
            if (!endereco.ehValido()) return;
            Endereco = endereco;
        }

        public void AtribuirCategoria(Categoria categoria)
        {
            if (!categoria.ehValido()) return;
            Categoria = categoria;
        }

        public void ExcluirEvento()
        {
            // TODO: deve validar alguma regra?
            Excluido = true;
        }

        //Validação de atributos (FluentValidation)
        public override bool ehValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }
        
        #region validacoes
        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();
            ValidationResult = Validate(this);

            //validações Adicionais (add)
            ValidarEndereco();
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
               .NotEmpty().WithMessage("O nome e requisitado!")
               .Length(2, 150).WithMessage("O nome deve conter entre 2 e 150 caracteres!");
        }

        private void ValidarValor()
        {
            if (!Gratuito)
            {
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 50000).WithMessage("O valor deve estar entre R$1,00 e R$50.000,00!");
            }
            if (Gratuito)
            {
                RuleFor(c => c.Valor)
                    .LessThanOrEqualTo(0).When(e => e.Gratuito)
                    .WithMessage("Eventos gratuitos nao devem conter valores acima de R$0,00!");
            }
        }

        private void ValidarData()
        {
            RuleFor(c => c.DataInicio)
                .GreaterThan(c => c.DataFim)
                .WithMessage("A data de inicio deve ser maior que a data final do evento!");

            RuleFor(c => c.DataInicio)
                .LessThan(DateTime.Now)
                .WithMessage("A data de inicio nao deve ser menor que a data atual!");
        }

        private void ValidarLocal()
        {
            if (Online)
            {
                RuleFor(c => c.Endereco)
                    .Null().When(c => c.Online).WithMessage("Eventos online nao devem possuir endereço!");
            }
            if (!Online)
            {
                RuleFor(c => c.Endereco)
                    .NotNull().When(c => c.Online == false)
                    .WithMessage("O endereco e requisitado!");
            }
        }

        private void ValidarNomeEmpresa()
        {
            RuleFor(c => c.NomeEmpresa)
                .NotEmpty().WithMessage("O nome da empresa e requisitado!")
                .Length(2, 150).WithMessage("O nome da empresa deve conter entre 2 e 150 caracteres!");
        }

        private void ValidarEndereco()
        {
            if (Online) return;
            if (Endereco.ehValido()) return;
            foreach(var error in Endereco.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

        #endregion

        public static class EventoFactory
        {
            public static Evento NovoEventoCompleto(Guid id, 
                string nome, 
                string descCurta, 
                string descLonga, 
                DateTime dataInicio, 
                DateTime dataFim, 
                bool gratuito, 
                decimal valor, 
                bool online, 
                string nomeEmpresa, 
                Guid? organizadorId,
                Endereco endereco,
                Guid categoriaId)
            {
                var evento = new Evento() {
                    Id = id,
                    Nome = nome,
                    DescricaoCurta = descCurta,
                    DescricaoLonga = descLonga,
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Gratuito = gratuito,
                    Valor = valor,
                    Online = online,
                    NomeEmpresa = nomeEmpresa,
                    Endereco = endereco,
                    CategoriaId = categoriaId
                };
                if (organizadorId.HasValue)
                {
                    evento.OrganizadorId = organizadorId.Value;
                }
                if (online)
                {
                    evento.Endereco = null;
                }
                return evento;
            }
        }
    }
}