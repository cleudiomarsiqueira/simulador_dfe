namespace SimuladorDFe.Modelos;

public sealed record RespostaEnvio(int CodigoStatus, string DescricaoStatus, string Conteudo, TimeSpan Duracao)
{
    public bool Sucesso => CodigoStatus is >= 200 and < 300;
}
