using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries
{
    public class BuscarUsuarioPorIdQueryHandler : IRequestHandler<BuscarUsuarioPorIdQuery, UsuarioDto>
    {
        private readonly DataContext _Context;
        
        public BuscarUsuarioPorIdQueryHandler(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public async Task<UsuarioDto> Handle(BuscarUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _Context.Usuarios.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }

            return UsuarioDto.From(usuario);
        }
    }
}
