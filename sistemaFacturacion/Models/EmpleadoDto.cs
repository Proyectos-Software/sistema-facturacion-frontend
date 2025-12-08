using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models;

public sealed class EmpleadoDto
{
    [JsonPropertyName("idEmp")] public int IdEmp { get; init; }
    [JsonPropertyName("idUsu")] public int IdUsu { get; init; }
    [JsonPropertyName("carEmp")] public string CarEmp { get; init; } = "";
    [JsonPropertyName("suelEmp")] public decimal SueEmp { get; init; }
    [JsonPropertyName("fecIng")] public DateTimeOffset? FecEmp { get; init; }
    [JsonPropertyName("estEmp")] public bool EstEmp { get; init; }
    [JsonPropertyName("codEmp")] public string CodEmp { get; init; } = "";
}

