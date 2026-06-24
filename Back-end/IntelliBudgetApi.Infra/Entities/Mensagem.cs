using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Infra.Entities
{
    public class Mensagem
    {
        public int Id { get; set; }
        public int ConversaId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime CradaEm { get; set; }
        public Conversa Conversa { get; set; } = null!;
    }
}
