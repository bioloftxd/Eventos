using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }

        public SelectList Categorias()
        {
            return new SelectList(ListarCategorias(), "Id", "Nome");
        }

        public List<CategoriaViewModel> ListarCategorias()
        {
            var categoriaList = new List<CategoriaViewModel>()
            {
                new CategoriaViewModel(){Id = new Guid("ae2cdee5-05e2-4b07-9091-cab5898a5c99"), Nome="Congresso"},
                new CategoriaViewModel(){Id = new Guid("0b5d2571-9087-4aa7-8276-513e9c535150"), Nome="Meetup"},
                new CategoriaViewModel(){Id = new Guid("c3aae1ef-7035-44db-9c33-04260f1c5f77"), Nome="Workshop"},
            };
            return categoriaList;
        }
    }
}
