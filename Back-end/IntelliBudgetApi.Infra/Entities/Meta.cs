using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Infra.Entities
{
    public class Meta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal ValorAlvo { get; set; }
        public DateTime Prazo { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
    }
}
