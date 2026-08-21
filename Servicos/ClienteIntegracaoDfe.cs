using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using SimuladorDFe.Modelos;

namespace SimuladorDFe.Servicos;

public sealed class ClienteIntegracaoDfe
{
    private static readonly JsonSerializerOptions OpcoesJson = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    private readonly HttpClient _httpClient;

    public ClienteIntegracaoDfe(HttpClient httpClient) => _httpClient = httpClient;

    public string SerializarRequisicao(RequisicaoRecepcaoDfe requisicao) =>
        JsonSerializer.Serialize(requisicao, OpcoesJson);

    public async Task<RespostaEnvio> EnviarAsync(
        Uri endereco,
        string token,
        RequisicaoRecepcaoDfe requisicao,
        CancellationToken cancellationToken)
    {
        return await EnviarAsync(
            HttpMethod.Post,
            endereco,
            token,
            new StringContent(SerializarRequisicao(requisicao), Encoding.UTF8, "application/json"),
            cancellationToken);
    }

    public Task<RespostaEnvio> ColetarPendentesAsync(
        Uri endereco,
        string token,
        CancellationToken cancellationToken) =>
        EnviarAsync(HttpMethod.Get, endereco, token, null, cancellationToken);

    public Task<RespostaEnvio> ConfirmarAcknowledgementAsync(
        Uri endereco,
        string token,
        CancellationToken cancellationToken) =>
        EnviarAsync(HttpMethod.Post, endereco, token, null, cancellationToken);

    private async Task<RespostaEnvio> EnviarAsync(
        HttpMethod metodo,
        Uri endereco,
        string token,
        HttpContent? corpoRequisicao,
        CancellationToken cancellationToken)
    {
        using var mensagem = new HttpRequestMessage(metodo, endereco)
        {
            Content = corpoRequisicao
        };

        var tokenSemPrefixo = token.Trim();
        if (!string.IsNullOrWhiteSpace(tokenSemPrefixo))
        {
            if (tokenSemPrefixo.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                tokenSemPrefixo = tokenSemPrefixo[7..].Trim();

            mensagem.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenSemPrefixo);
        }

        var cronometro = Stopwatch.StartNew();
        using var resposta = await _httpClient.SendAsync(mensagem, cancellationToken);
        var corpoResposta = await resposta.Content.ReadAsStringAsync(cancellationToken);
        cronometro.Stop();

        return new RespostaEnvio(
            (int)resposta.StatusCode,
            resposta.ReasonPhrase ?? string.Empty,
            corpoResposta,
            cronometro.Elapsed);
    }
}
