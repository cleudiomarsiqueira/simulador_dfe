namespace SimuladorDFe.Modelos;

public sealed record ConfiguracaoDfe(
    string UrlBaseApi,
    TipoDocumento TipoDocumento,
    FluxoDocumento FluxoDocumento,
    int? SequenciaDfe,
    string? PontoImpressao,
    string Token)
{
    public static ConfiguracaoDfe Padrao { get; } = new(
        UrlBaseApi: string.Empty,
        TipoDocumento: TipoDocumento.NFe,
        FluxoDocumento: FluxoDocumento.Recepcao,
        SequenciaDfe: null,
        PontoImpressao: null,
        Token: string.Empty);
}

public static class RoteamentoDfe
{
    public static string ObterCaminhoRetorno(TipoDocumento tipoDocumento, FluxoDocumento fluxoDocumento) => (tipoDocumento, fluxoDocumento) switch
    {
        (TipoDocumento.NFe, FluxoDocumento.Emissao) => "/api/v1/nfe/emissao/documentos/retorno",
        (TipoDocumento.NFe, FluxoDocumento.Recepcao) => "/api/v1/nfe/recepcao/documentos/retorno",
        (TipoDocumento.CTe, FluxoDocumento.Emissao) => "/api/v1/cte/emissao/documentos/retorno",
        (TipoDocumento.CTe, FluxoDocumento.Recepcao) => "/api/v1/cte/recepcao/documentos/retorno",
        (TipoDocumento.NFSe, FluxoDocumento.Emissao) => "/api/v1/nfse/emissao/documentos/retorno",
        (TipoDocumento.NFSe, FluxoDocumento.Recepcao) => "/api/v1/nfse/recepcao/documentos/retorno",
        (TipoDocumento.MDFe, FluxoDocumento.Emissao) => "/api/v1/mdfe/emissao/documentos/retorno",
        _ => throw new ArgumentOutOfRangeException(nameof(fluxoDocumento))
    };

    public static string ObterCaminhoColeta(TipoDocumento tipoDocumento) =>
        $"{ObterCaminhoBaseEmissao(tipoDocumento)}/pendentes";

    public static string ObterCaminhoAcknowledgement(TipoDocumento tipoDocumento, int sequenciaDfe) =>
        $"{ObterCaminhoBaseEmissao(tipoDocumento)}/{sequenciaDfe}/ack";

    public static Uri MontarEndereco(string urlBaseApi, string caminhoEndpoint)
    {
        var urlPublicacao = urlBaseApi.Trim().TrimEnd('/');
        if (!Uri.TryCreate(urlPublicacao, UriKind.Absolute, out var publicacao) ||
            publicacao.Scheme is not ("http" or "https") ||
            !string.IsNullOrEmpty(publicacao.Query) || !string.IsNullOrEmpty(publicacao.Fragment))
            throw new InvalidOperationException("Informe somente a URL base HTTP ou HTTPS da publicação, sem rota, parâmetros ou fragmentos.");

        return new Uri(urlPublicacao + caminhoEndpoint, UriKind.Absolute);
    }

    private static string ObterCaminhoBaseEmissao(TipoDocumento tipoDocumento) => tipoDocumento switch
    {
        TipoDocumento.NFe => "/api/v1/nfe/emissao/documentos",
        TipoDocumento.CTe => "/api/v1/cte/emissao/documentos",
        TipoDocumento.NFSe => "/api/v1/nfse/emissao/documentos",
        TipoDocumento.MDFe => "/api/v1/mdfe/emissao/documentos",
        _ => throw new ArgumentOutOfRangeException(nameof(tipoDocumento))
    };
}
