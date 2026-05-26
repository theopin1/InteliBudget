using IntelliBudgetApi.Infra.Entities;
using IntelliBudgetApi.Infra.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.DTO
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Essencial { get; set; }
        public static CategoriaDto From(Categoria categoria)
        {
            return new CategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Essencial = categoria.Essencial,
            };
        }
    }
}
