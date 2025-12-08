using System.Text.Json.Serialization;

public class ClienteDto
{
    [JsonPropertyName("idCli")] public int IdCli { get; set; }
    [JsonPropertyName("nomCli")] public string? NomCli { get; set; }
    [JsonPropertyName("apeCli")] public string? ApeCli { get; set; }
    [JsonPropertyName("ideCli")] public string? IdeCli { get; set; }
    [JsonPropertyName("tipCli")] public string? TipCli { get; set; }
    [JsonPropertyName("dirCli")] public string? DirCli { get; set; }
    [JsonPropertyName("telCli")] public string? TelCli { get; set; }
    [JsonPropertyName("corCli")] public string? CorCli { get; set; }
    [JsonPropertyName("fecCli")] public DateTimeOffset FecCli { get; set; }
    [JsonPropertyName("estCli")] public bool EstCli { get; set; }
    [JsonPropertyName("codCli")] public string? CodCli { get; set; }
    //[JsonPropertyName("facturas")] public List<FacturaDto>? Facturas { get; set; }
}