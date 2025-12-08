using System.Text.Json.Serialization;

public sealed class UsuarioDto
{
    [JsonPropertyName("idUsu")] public int IdUsu { get; init; }
    [JsonPropertyName("nomUsu")] public string NomUsu { get; init; } = "";
    [JsonPropertyName("apeUsu")] public string ApeUsu { get; init; } = "";
    [JsonPropertyName("corUsu")] public string CorUsu { get; init; } = "";
    [JsonPropertyName("telUsu")] public string TelUsu { get; init; } = "";
    [JsonPropertyName("idRol")] public int IdRol { get; init; }
    [JsonPropertyName("estUsu")] public bool EstUsu { get; init; }
    [JsonPropertyName("carEmp")] public string? CarEmp { get; init; }
    [JsonPropertyName("suelEmp")] public decimal? SueEmp { get; init; }
    [JsonPropertyName("fecCre")] public DateTimeOffset? FecCre { get; init; }
}
