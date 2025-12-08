using System.Text.Json.Serialization;

public sealed class CreateEmpleadoRequest
{
    [JsonPropertyName("nomUsu")] public string NomUsu { get; init; } = "";
    [JsonPropertyName("apeUsu")] public string ApeUsu { get; init; } = "";
    [JsonPropertyName("corUsu")] public string CorUsu { get; init; } = "";
    [JsonPropertyName("telUsu")] public string TelUsu { get; init; } = "";
    [JsonPropertyName("carEmp")] public string CarEmp { get; init; } = "";
    [JsonPropertyName("suelEmp")] public decimal SueEmp { get; init; }
}
