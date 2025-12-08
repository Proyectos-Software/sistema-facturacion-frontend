using System.Text.Json.Serialization;

public class ClienteCreateRequest
{
    [JsonPropertyName("nombre")] public string Nombre { get; set; } = "";
    [JsonPropertyName("apellido")] public string Apellido { get; set; } = "";
    [JsonPropertyName("identificacion")] public string Identificacion { get; set; } = "";
    [JsonPropertyName("tipoIdentificacion")] public string TipoIdentificacion { get; set; } = "";
    [JsonPropertyName("direccion")] public string Direccion { get; set; } = "";
    [JsonPropertyName("telefono")] public string Telefono { get; set; } = "";
    [JsonPropertyName("correoElectronico")] public string CorreoElectronico { get; set; } = "";
    [JsonPropertyName("fechaCreacion")] public DateTime? FechaCreacion {  get; set; } = DateTime.Now;
    [JsonPropertyName("estado")] public Boolean? EstadoCliente { get; set; } = false;
}
