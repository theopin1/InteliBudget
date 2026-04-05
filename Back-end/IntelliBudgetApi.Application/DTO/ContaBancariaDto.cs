using IntelliBudgetApi.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.DTO
{
    public class ContaBancariaDto
    {
        public int Id { get; set; }
        public string? NomeBanco { get; set; }
        public string? TipoConta { get; set; }
        public Usuario? Usuario { get; set; }
        public int Saldo { get; set; }
    }
}
