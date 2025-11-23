using System.Text.Json.Serialization;
using System.Collections.Generic;

public class FacturaCreateRequest
{
    [JsonPropertyName("idCli")]
    public int IdCli { get; set; }

    [JsonPropertyName("idEmp")]
    public int IdEmp { get; set; }

    [JsonPropertyName("idUsu")]
    public int IdUsu { get; set; }

    [JsonPropertyName("detalles")]
    public List<DetalleFacturaCreateRequest> Detalles { get; set; } = new();
}

public class DetalleFacturaCreateRequest
{
    [JsonPropertyName("idPro")]
    public int IdPro { get; set; }

    [JsonPropertyName("idLot")]
    public int IdLot { get; set; }

    [JsonPropertyName("cantidad")]
    public int Cantidad { get; set; }
}
