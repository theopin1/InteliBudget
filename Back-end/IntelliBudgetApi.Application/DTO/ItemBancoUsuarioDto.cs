using IntelliBudgetApi.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.DTO
{
    public class ItemBancoUsuarioDto
    {
        public int Id { get; set; }
        public string? Status { get; set; }

        public static ItemBancoUsuarioDto From(ItemBancoUsuario itemBancoUsuario)
        {
            return new ItemBancoUsuarioDto
            {
                Id = itemBancoUsuario.Id,
                Status = itemBancoUsuario.Status,
            };
            
        }
    }
}
