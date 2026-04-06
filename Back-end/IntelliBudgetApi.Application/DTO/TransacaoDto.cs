using IntelliBudgetApi.Infra.Entities;
using IntelliBudgetApi.Infra.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.DTO
{
    public class TransacaoDto
    {
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime DataTransacao { get; set; }
        public int Valor { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public int ContaBancariaId { get; set; }
        public ContaBancaria? ContaBancaria { get; set; }

        public static TransacaoDto From(Transacao transacao)
        {
            return new TransacaoDto
            {
                Id = transacao.Id,
                Tipo = transacao.Tipo,
                DataTransacao = transacao.DataTransacao,
                Valor = transacao.Valor,
                Categoria = transacao.Categoria,
                ContaBancaria = transacao.ContaBancaria
            };
        }
    }
}
