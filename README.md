# Simulador DFe

Aplicativo Windows para testar os endpoints de retorno e de coleta da Integração DFe em ambientes autorizados.

## Uso

1. Primeiro, preencha a janela `Ferramentas > Configurar API` com a URL base da publicação, como `https://servidor/IntegracaoDFeApi`, e selecione `Salvar`. Enquanto a URL não for validada, as demais funções ficam bloqueadas.
2. O token Bearer é opcional e não é gravado em arquivo. A API liberada para teste sem token aceita somente o callback de retorno MDF-e; coleta e ACK continuam exigindo autenticação.
3. Na aba `Recepção`, escolha o tipo e a origem: `Emitido pelo ERP` para retornar o processamento de documento originado no ERP — incluindo a gravação em `DFE_RETORNO` — ou `Recebido de Terceiro` para receber um documento novo de terceiro. `Emitido pelo ERP` é o padrão. MDF-e possui somente essa origem.
4. Ainda nessa aba, abra, arraste ou cole o documento. Para XML, o tipo é identificado automaticamente pela raiz (`nfeProc`, `cteProc`, `cteSimpProc` ou `mdfeProc`) e a rota é recalculada; para JSON válido, o tipo selecionado é NFS-e. Ao carregar, o conteúdo é formatado e a aba `Editar Valores` já fica pronta para uso.
5. Para XML, utilize a árvore de campos para selecionar uma tag e alterar seu valor ou atributos. Cada edição atualiza automaticamente a prévia, o JSON formatado e o corpo do POST.
6. Use `Simular Recepção` para enviar o conteúdo assim que a prévia automática estiver válida.
7. Quando um XML tiver alterações, `Salvar XML Alterado` fica disponível para gravar o conteúdo editado em outro arquivo.
8. Na aba `Emissão`, escolha somente o tipo documental e use `Simular Emissão`. A resposta da API é exibida e, quando houver uma única sequência, ela é sugerida no diálogo de confirmação de recebimento. Essa aba não usa XML nem altera os parâmetros do retorno.
9. Use `Confirmar Recebimento` somente depois de conferir a sequência: o diálogo permite informar qual documento retornado na coleta receberá a confirmação. O ACK é uma ação explícita porque altera o status apenas do documento escolhido.

As rotas são fixas e calculadas automaticamente a partir da URL base:

- NF-e emissão: `/api/v1/nfe/emissao/documentos/retorno`; recepção: `/api/v1/nfe/recepcao/documentos/retorno`;
- CT-e emissão: `/api/v1/cte/emissao/documentos/retorno`; recepção: `/api/v1/cte/recepcao/documentos/retorno`;
- NFS-e emissão: `/api/v1/nfse/emissao/documentos/retorno`; recepção: `/api/v1/nfse/recepcao/documentos/retorno`;
- MDF-e emissão: `/api/v1/mdfe/emissao/documentos/retorno`.

Para os quatro documentos, a coleta de emissão usa o mesmo padrão de rotas:

- coleta: `/api/v1/{tipo}/emissao/documentos/pendentes`;
- acknowledgement: `/api/v1/{tipo}/emissao/documentos/{seqIdentificaDfe}/ack`.

Os caminhos são iguais, mas o JSON devolvido pela coleta é específico de cada tipo documental. Recepção, eventos, inutilização e CIOT possuem fluxos próprios e não são simulados por esses botões.

Para NF-e, o arquivo deve ser um XML `nfeProc`. Para CT-e, o XML pode ser `cteProc` ou `cteSimpProc`. Para MDF-e, selecione o XML de retorno `mdfeProc`. A transformação é a mesma dos builders de teste da API: remove o `xmlns` padrão, converte com `JsonConvert.SerializeXmlNode`, compacta GZip UTF-8 e codifica em Base64.

Para NFS-e, a API atual recebe JSON compactado; portanto a ferramenta solicita um JSON, não XML.

Alterações em documentos fiscais assinados invalidam a assinatura digital. Esse comportamento é esperado para testes de mapeamento e persistência, mas não substitui testes de assinatura.

## Atalhos

- `F2` ou `Ctrl+O`: abrir documento;
- `F3`: abrir a configuração da API;
- `F8`: confirmar recebimento da sequência DFe;
- `F10`: simular recepção ou emissão, conforme a aba ativa;
- `F12`: limpar documento e resultados.

O retorno `2xx` confirma o aceite pela API. A sincronização posterior para o ERP pode ocorrer de forma assíncrona pelo worker de retorno. Em ambiente compartilhado, o POST pode alterar o status do documento no ERP; use apenas documento de teste e informe token quando o endpoint o exigir.

Cada chamada HTTP possui limite de cinco segundos. Caso a API não responda nesse prazo, a operação é cancelada e o indicador do rodapé fica vermelho.

## Publicação

Abra `SimuladorDFe.sln` no Visual Studio e use o perfil `Windows-x64`, ou execute:

```powershell
dotnet publish .\SimuladorDFe.csproj -c Release -r win-x64 --self-contained true
```
