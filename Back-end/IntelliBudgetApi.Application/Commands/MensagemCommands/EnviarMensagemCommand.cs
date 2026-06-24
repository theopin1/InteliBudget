using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ChatBotCommands
{
    public record EnviarMensagemCommand(    
    int UsuarioId,
    string Mensagem,
    List<MensagemDto> Historico
) : IRequest<EnviarMensagemResult>;

    public record EnviarMensagemResult(string Resposta);
}
