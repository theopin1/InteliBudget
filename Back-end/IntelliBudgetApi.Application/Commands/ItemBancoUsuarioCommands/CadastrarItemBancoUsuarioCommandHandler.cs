using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ItemBancoUsuarioCommands
{
    public class CadastrarItemBancoUsuarioCommandHandler : IRequestHandler<CadastrarItemBancoUsuarioCommand, ItemBancoUsuarioDto>
    {
        private readonly DataContext _context;

        public CadastrarItemBancoUsuarioCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<ItemBancoUsuarioDto> Handle(CadastrarItemBancoUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("ItemBancoUsuario não pode ser vazio");
            }

            var itemBancoUsuario = new ItemBancoUsuario
            {
                UserId = request.UserId,
                ItemId = request.ItemId,
                Status = request.Status,
            };

            await _context.itemBancoUsuarios.AddAsync(itemBancoUsuario);
            await _context.SaveChangesAsync(cancellationToken);

            return ItemBancoUsuarioDto.From(itemBancoUsuario);
        }
    }
}
