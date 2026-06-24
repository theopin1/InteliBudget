using IntelliBudgetApi.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.DTO
{
    public class MensagemDto
    {
        public string Role { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
    }
}
