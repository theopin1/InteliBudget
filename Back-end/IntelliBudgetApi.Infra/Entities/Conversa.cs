using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Infra.Entities
{
    public class Conversa
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? Titulo { get; set; }
        public DateTime CriadaEm { get; set; }

        public ICollection<Mensagem> Mensagens { get; set; }
    }
}
