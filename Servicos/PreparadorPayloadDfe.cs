using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimuladorDFe.Modelos;

namespace SimuladorDFe.Servicos;

/// <summary>
/// Prepara o conteúdo no mesmo formato utilizado pelos testes de retorno da API:
/// XML sem namespace padrão, JSON via Newtonsoft, GZip UTF-8 e Base64.
/// </summary>
public sealed class PreparadorPayloadDfe
{
    public PayloadPreparado Preparar(TipoDocumento tipoDocumento, string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
            throw new ArgumentException("Selecione um arquivo com conteúdo.", nameof(conteudo));

        var json = tipoDocumento switch
        {
            TipoDocumento.NFe => ConverterXmlEmJson(conteudo, "nfeProc"),
            TipoDocumento.CTe => ConverterXmlEmJson(conteudo, "cteProc", "cteSimpProc"),
            TipoDocumento.NFSe => NormalizarJsonNfse(conteudo),
            TipoDocumento.MDFe => ConverterXmlEmJson(conteudo, "mdfeProc"),
            _ => throw new ArgumentOutOfRangeException(nameof(tipoDocumento))
        };

        var compactado = Convert.ToBase64String(CompactarGzip(json));
        ValidarIntegridade(json, compactado);

        return new PayloadPreparado(json, compactado);
    }

    private static string ConverterXmlEmJson(string xml, params string[] raizesEsperadas)
    {
        // Esta remoção é intencional: os builders de teste de NF-e, CT-e e MDF-e da API
        // removem xmlns antes de serializar, para que os mappers localizem as tags.
        var xmlSemNamespace = Regex.Replace(xml, @"\s+xmlns=""[^""]+""", string.Empty);
        var documento = new XmlDocument();
        documento.LoadXml(xmlSemNamespace);

        var raiz = documento.DocumentElement?.Name;
        if (!raizesEsperadas.Contains(raiz, StringComparer.Ordinal))
        {
            var esperados = string.Join("' ou '", raizesEsperadas);
            throw new InvalidOperationException($"O arquivo deve conter o elemento raiz '{esperados}', mas contém '{raiz ?? "nenhum"}'.");
        }

        return JsonConvert.SerializeXmlNode(documento, Newtonsoft.Json.Formatting.None);
    }

    private static string NormalizarJsonNfse(string json)
    {
        var token = JToken.Parse(json);
        if (token is not JObject && token is not JArray)
            throw new InvalidOperationException("O arquivo de NFS-e deve conter um objeto ou uma lista JSON.");

        return token.ToString(Newtonsoft.Json.Formatting.None);
    }

    // Equivalente a IntegracaoDFeApi.Dominio.Extensions.CompactarExtension.CompactarGzip.
    private static byte[] CompactarGzip(string texto)
    {
        var bytes = Encoding.UTF8.GetBytes(texto);
        using var memoria = new MemoryStream();
        using (var gzip = new GZipStream(memoria, CompressionMode.Compress, leaveOpen: true))
            gzip.Write(bytes, 0, bytes.Length);

        return memoria.ToArray();
    }

    // Equivalente a DescompactarBase64Gzip, usado para conferir localmente o payload antes do POST.
    private static string DescompactarBase64Gzip(string base64)
    {
        try
        {
            var gzipBytes = Convert.FromBase64String(base64);
            using var memoria = new MemoryStream(gzipBytes);
            using var gzip = new GZipStream(memoria, CompressionMode.Decompress);
            using var saida = new MemoryStream();
            gzip.CopyTo(saida);

            var bytes = saida.ToArray();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var resultado = Encoding.UTF8.GetString(bytes);
            return resultado.Contains('�')
                ? Encoding.GetEncoding("ISO-8859-1").GetString(bytes)
                : resultado;
        }
        catch
        {
            var gzipBytes = Convert.FromBase64String(base64);
            using var memoria = new MemoryStream(gzipBytes);
            using var gzip = new GZipStream(memoria, CompressionMode.Decompress);
            using var leitor = new StreamReader(gzip, Encoding.UTF8);
            return leitor.ReadToEnd();
        }
    }

    private static void ValidarIntegridade(string jsonOriginal, string base64)
    {
        var jsonDescompactado = DescompactarBase64Gzip(base64);
        if (!string.Equals(jsonOriginal, jsonDescompactado, StringComparison.Ordinal))
            throw new InvalidOperationException("A validação local do payload GZip/Base64 falhou.");
    }
}
