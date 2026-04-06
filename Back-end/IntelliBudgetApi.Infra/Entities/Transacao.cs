using IntelliBudgetApi.Infra.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Infra.Entities
{
    public class Transacao
    {
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime DataTransacao { get; set; }
        public int Valor { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public int ContaBancariaId { get; set; }
        public ContaBancaria? ContaBancaria { get; set; }
    }
}
