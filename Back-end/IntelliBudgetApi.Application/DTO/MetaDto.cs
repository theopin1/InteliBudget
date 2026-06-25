using IntelliBudgetApi.Infra.Entities;

namespace IntelliBudgetApi.Application.DTO
{
    public class MetaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal ValorAlvo { get; set; }
        public DateTime Prazo { get; set; }

        public static MetaDto from(Meta meta) {
            return new MetaDto
            {
                Id = meta.Id,
                Nome = meta.Nome,
                Descricao = meta.Descricao,
                ValorAtual = meta.ValorAtual,
                ValorAlvo = meta.ValorAlvo,
                Prazo = meta.Prazo
            };
        }
    }
}
