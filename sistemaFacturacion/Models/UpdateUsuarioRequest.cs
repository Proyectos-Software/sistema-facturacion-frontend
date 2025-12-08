using System.Text.Json.Serialization;

public sealed class UpdateUsuarioRequest
{
    [JsonPropertyName("nomUsu")] public string NomUsu { get; init; } = "";
    [JsonPropertyName("apeUsu")] public string ApeUsu { get; init; } = "";
    [JsonPropertyName("corUsu")] public string CorUsu { get; init; } = "";
    [JsonPropertyName("telUsu")] public string TelUsu { get; init; } = "";
}
