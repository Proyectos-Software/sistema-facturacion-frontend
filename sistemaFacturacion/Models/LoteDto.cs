using System.Text.Json.Serialization;

public class LoteDto
{
    [JsonPropertyName("idLot")]
    public int Id { get; set; }

    [JsonPropertyName("idPro")]
    public int IdPro { get; set; }

    [JsonPropertyName("nombreProducto")]
    public string? NombreProducto { get; set; }

    [JsonPropertyName("nomCatPro")]
    public string? NombreCategoria { get; set; }

    [JsonPropertyName("codLot")]
    public string? CodLot { get; set; }

    [JsonPropertyName("fecCom")]
    public DateTime FecCom { get; set; }

    [JsonPropertyName("fecExp")]
    public DateTime FecExp { get; set; }

    [JsonPropertyName("canLot")]
    public int CanLot { get; set; }

    [JsonPropertyName("canDis")]
    public int CanDis { get; set; }

    [JsonPropertyName("preCom")]
    public decimal PreCom { get; set; }

    [JsonPropertyName("preVen")]
    public decimal PreVen { get; set; }

    [JsonPropertyName("estLot")]
    public bool EstLot { get; set; }
}
