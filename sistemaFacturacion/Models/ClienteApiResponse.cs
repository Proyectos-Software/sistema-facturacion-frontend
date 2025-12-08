using System.Text.Json.Serialization;

public class ClienteApiResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }

    [JsonPropertyName("apellido")]
    public string? Apellido { get; set; }

    [JsonPropertyName("identificacion")]
    public string? Identificacion { get; set; }

    [JsonPropertyName("tipoIdentificacion")]
    public string? TipoIdentificacion { get; set; }

    [JsonPropertyName("direccion")]
    public string? Direccion { get; set; }

    [JsonPropertyName("telefono")]
    public string? Telefono { get; set; }

    [JsonPropertyName("correoElectronico")]
    public string? CorreoElectronico { get; set; }

    [JsonPropertyName("fechaCreacion")]
    public DateTimeOffset FechaCreacion { get; set; }

    [JsonPropertyName("estado")]
    public bool Estado { get; set; }
    [JsonPropertyName("codigoCliente")]
    public string CodigoCliente { get; set; }
}