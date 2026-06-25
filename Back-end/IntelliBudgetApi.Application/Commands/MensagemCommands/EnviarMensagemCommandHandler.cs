using MediatR;
using Microsoft.Extensions.AI;
namespace IntelliBudgetApi.Application.Commands.ChatBotCommands
{
    public class EnviarMensagemCommandHandler : IRequestHandler<EnviarMensagemCommand, EnviarMensagemResult>
    {
        private readonly IChatClient _chatClient;
        private const string SystemPrompt = """
        Você é o IntelliBudget AI, um assistente financeiro pessoal inteligente e empático.
        Seu objetivo é ajudar o usuário a:
        - Entender conceitos financeiros de forma simples e clara
        - Criar e acompanhar metas financeiras (economia, pagamento de dívidas, investimentos)
        - Desenvolver hábitos saudáveis de finanças pessoais
        - Planejar orçamentos mensais
        - Entender investimentos básicos (renda fixa, renda variável, Tesouro Direto, etc.)
        - Tomar decisões financeiras mais conscientes
        Regras de comportamento:
        - Responda sempre em português brasileiro
        - Seja objetivo, amigável e encorajador
        - Use exemplos práticos e números concretos quando possível
        - Nunca invente dados financeiros do usuário — você não tem acesso a eles
        - Se o usuário perguntar sobre seus gastos ou saldo, explique que você não tem acesso a esses dados e oriente-o a verificar no próprio aplicativo
        - Evite jargões complexos; quando usar termos técnicos, explique-os
        - Não dê conselhos sobre temas fora de finanças pessoais
        """;
        public EnviarMensagemCommandHandler(IChatClient chatClient)
        {
            _chatClient = chatClient;
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
            var response = await _chatClient.GetResponseAsync(mensagens, cancellationToken: cancellationToken);
            return new EnviarMensagemResult(response.Text);
        }
    }
}