using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Entities;
using IntelliBudgetApi.Infra.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.TransacaoCommands
{
    public class AtualizarTransacaoCommand : IRequest<TransacaoDto>
    {
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime DataTransacao { get; set; }
        public int Valor { get; set; }
        public int? CategoriaId { get; set; }
  

        public void Atualizar(Transacao transacao)
        {
            transacao.Id = Id;
            transacao.Tipo = Tipo;
            transacao.DataTransacao = DataTransacao;
            transacao.Valor = Valor;
            transacao.CategoriaId = CategoriaId; 
        }
    }
}
