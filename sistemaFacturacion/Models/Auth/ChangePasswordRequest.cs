using System.Text.Json.Serialization;

public sealed class ChangePasswordRequest
{
    [JsonPropertyName("contrasenaActual")] public string ContrasenaActual { get; init; } = "";
    [JsonPropertyName("contrasenaNueva")] public string ContrasenaNueva { get; init; } = "";
}
