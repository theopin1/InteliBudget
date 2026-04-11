using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ItemBancoUsuarioCommands
{
    public class CadastrarItemBancoUsuarioCommand : IRequest<ItemBancoUsuarioDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ItemId { get; set; }
        public string? Status { get; set; }
    }
}
