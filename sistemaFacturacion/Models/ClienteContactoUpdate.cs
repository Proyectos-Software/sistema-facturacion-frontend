using System.Text.Json.Serialization;

public class ClienteContactoUpdate
{
    [JsonPropertyName("direccion")] public string Direccion { get; set; } = "";
    [JsonPropertyName("telefono")] public string Telefono { get; set; } = "";
    [JsonPropertyName("correoElectronico")] public string CorreoElectronico { get; set; } = "";
}
