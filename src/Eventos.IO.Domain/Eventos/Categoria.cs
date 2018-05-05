using Eventos.IO.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos
{
    public class Categoria : Entity<Categoria>
    {
        public Categoria(Guid id)
        {
            Id = id;
        }
        //EF propriedade de navegação
        public string Nome { get; private set; }
        public virtual ICollection<Evento> Eventos { get; set; }

        //Construtor para o EF
        protected Categoria() { }

        public override bool ehValido()
        {
            return true;
        }
    }
}