using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ItemBancoUsuarioCommands
{
    public class SyncContaCommand : IRequest<Unit>
    {
        public string ItemId { get; set; }
        public int UserId { get; set; }
    }
}
