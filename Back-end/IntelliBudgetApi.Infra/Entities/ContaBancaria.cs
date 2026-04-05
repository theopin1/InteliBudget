using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Infra.Entities
{
    public class ContaBancaria
    {
        public int Id { get; set; }
        public string? NomeBanco { get; set; }
        public string? TipoConta { get; set; }
        public Usuario? Usuario { get; set; }
        public int Saldo { get; set; }
    }
}
