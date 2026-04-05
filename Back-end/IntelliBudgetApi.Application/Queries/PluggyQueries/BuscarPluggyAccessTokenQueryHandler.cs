using IntelliBudgetApi.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.PluggyQueries
{
    public class BuscarPluggyAccessTokenQueryHandler : IRequestHandler<BuscarPluggyAccessTokenQuery, string>
    {
        private  readonly PluggyService _pluggyService;

        public BuscarPluggyAccessTokenQueryHandler(PluggyService pluggyService)
        {
            _pluggyService = pluggyService;
        }

        public async Task<string> Handle(BuscarPluggyAccessTokenQuery request, CancellationToken cancellationToken)
        {
            return await _pluggyService.GetConnectTokenAsync();
        }
    }
}
