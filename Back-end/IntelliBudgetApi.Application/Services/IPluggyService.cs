using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Services
{
    public interface IPluggyService
    {
        Task<string> GetApiKeyAsync();
        Task<string> GetConnectTokenAsync();
    }
}
