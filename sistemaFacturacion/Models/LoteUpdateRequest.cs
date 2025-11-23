using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class LoteUpdateRequest
{
    [JsonPropertyName("idPro")]
    [Required]
    public int IdPro { get; set; }

    [JsonPropertyName("fecCom")]
    [Required]
    public DateTime FecCom { get; set; }

    [JsonPropertyName("fecExp")]
    [Required]
    public DateTime FecExp { get; set; }

    [JsonPropertyName("canLot")]
    [Range(1, int.MaxValue)]
    public int CanLot { get; set; }

    [JsonPropertyName("canDis")]
    [Range(0, int.MaxValue)]
    public int CanDis { get; set; }

    [JsonPropertyName("preCom")]
    [Range(0, 999999)]
    public decimal PreCom { get; set; }
}
