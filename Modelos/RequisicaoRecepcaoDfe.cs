namespace SimuladorDFe.Modelos;

public sealed class RequisicaoRecepcaoDfe
{
    public required string DocumentoJson { get; init; }
    public int? SeqIdentificaDfe { get; init; }
    public string? PontoImpressao { get; init; }
}
