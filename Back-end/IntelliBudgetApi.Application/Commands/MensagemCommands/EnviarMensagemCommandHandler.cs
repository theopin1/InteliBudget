using IntelliBudgetApi.Application.Queries.TransacaoQueries;
using MediatR;
using Microsoft.Extensions.AI;
using System.Text.Json;

namespace IntelliBudgetApi.Application.Commands.ChatBotCommands
{
    public class EnviarMensagemCommandHandler : IRequestHandler<EnviarMensagemCommand, EnviarMensagemResult>
    {
        private readonly IChatClient _chatClient;
        private readonly IMediator _mediator;

        private const string SystemPrompt = """
        Você é um assistente financeiro pessoal.
        Ao responder perguntas financeiras, SEMPRE busque os dados reais
        do usuário antes de responder — nunca invente valores.
        Ao sugerir uma meta, apresente a sugestão primeiro e aguarde
        confirmação explícita antes de criá-la.
        """;

        public EnviarMensagemCommandHandler(IChatClient chatClient, IMediator mediator)
        {
            _chatClient = chatClient;
            _mediator = mediator;
        }

        public async Task<EnviarMensagemResult> Handle(EnviarMensagemCommand request, CancellationToken cancellationToken)
        {
            var mensagens = new List<ChatMessage>
            {
                new(ChatRole.System, SystemPrompt)
            };

            mensagens.AddRange(request.Historico.Select(h =>
               new ChatMessage(
                   h.Role == "user" ? ChatRole.User : ChatRole.Assistant,
                   h.Conteudo
               )
            ));

            mensagens.Add(new ChatMessage(ChatRole.User, request.Mensagem));

            var tools = new List<AITool>
            {
               /*AIFunctionFactory.Create(
                    async(string titulo, decimal valorALvo, DateTime prazo)=>
                    {
                        await _mediator.Send(
                            new CriarMetaCommand(request.UsuarioId, titulo, valorAlvo, prazo)
                            );
                        return "Meta criada com sucesso!";
                    },
                     "criarMeta",
                    "Cria uma meta financeira. Use somente após confirmação do usuário."
                ),
               */

                AIFunctionFactory.Create(
                     async(int? quantidade, string? dataInicio, string? dataFim)=>
                     {
                         Console.WriteLine($"[TOOL CALL] buscarTransacoes iniciada. Quantidade: {quantidade}, DataInicio: {dataInicio}, DataFim: {dataFim}");
                         
                         DateTime? dtInicio = null;
                         DateTime? dtFim = null;
                         
                         if (!string.IsNullOrEmpty(dataInicio) && DateTime.TryParse(dataInicio, out var d1)) dtInicio = d1;
                         if (!string.IsNullOrEmpty(dataFim) && DateTime.TryParse(dataFim, out var d2)) dtFim = d2;

                         try 
                         {
                             var result = await _mediator.Send(
                                 new ListarTransacoesQuery 
                                 {
                                     UsuarioId = request.UsuarioId,
                                     Quantidade = quantidade ?? 15,
                                     DataInicio = dtInicio,
                                     DataFim = dtFim
                                 }
                             );

                             if (result == null || result.Count == 0)
                             {
                                 Console.WriteLine("[TOOL CALL] buscarTransacoes: Zero resultados encontrados.");
                                 return "Nenhuma transação encontrada com esses filtros.";
                             }

                             var simplificado = result.Select(t => new {
                                 t.Id,
                                 t.Tipo,
                                 Data = t.DataTransacao.ToString("yyyy-MM-dd"),
                                 t.Valor,
                                 Categoria = t.Categoria?.Nome,
                                 Conta = t.ContaBancaria?.NomeBanco
                             });

                             var jsonString = JsonSerializer.Serialize(simplificado);
                             Console.WriteLine($"[TOOL CALL] buscarTransacoes: Sucesso. Retornando JSON: {jsonString}");
                             return jsonString;
                         }
                         catch (Exception ex)
                         {
                             Console.WriteLine($"[TOOL CALL] buscarTransacoes ERRO: {ex.Message}");
                             return "Ocorreu um erro ao buscar as transacoes.";
                         }
                     },
                     "buscarTransacoes",
                     "Busca transacoes do usuario. Opcional: 'quantidade' (ex: 2), 'dataInicio' (YYYY-MM-DD) e 'dataFim' (YYYY-MM-DD)."
                )
            };

            var options = new ChatOptions { Tools = tools };
            var response = await _chatClient.GetResponseAsync(mensagens, options, cancellationToken);

            return new EnviarMensagemResult(response.Text); 
        }
    }
}
