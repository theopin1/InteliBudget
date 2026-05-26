using System;

public class TransacaoBuilder
{
    private readonly Transacao _transacao;

    public TransacaoBuilder()
    {
        _transacao = new Transacao
        {
            DataTransacao = DateTime.Now
        };
    }

    public TransacaoBuilder ComTipo(TipoTransacao tipo)
    {
        _transacao.Tipo = tipo;
        return this;
    }

    public TransacaoBuilder ComValor(int valor)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero");

        _transacao.Valor = valor;
        return this;
    }

    public TransacaoBuilder ComCategoria(int? categoriaId)
    {
        _transacao.CategoriaId = categoriaId;
        return this;
    }

    public TransacaoBuilder ComConta(int? contaId)
    {
        _transacao.ContaBancariaId = contaId;
        return this;
    }

    public Transacao Build()
    {
        // regra de negócio
        if (_transacao.Tipo == TipoTransacao.Saida && !_transacao.CategoriaId.HasValue)
            throw new Exception("Transações de saída precisam de categoria");

        return _transacao;
    }
}