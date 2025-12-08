using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models;

public sealed class UpdateEmpleadoRequest
{
    [JsonPropertyName("idUsu")] public int IdUsu { get; init; }
    [JsonPropertyName("carEmp")] public string CarEmp { get; init; } = "";
    [JsonPropertyName("suelEmp")] public decimal SueEmp { get; init; }
    [JsonPropertyName("estEmp")] public bool EstEmp { get; init; }
}

