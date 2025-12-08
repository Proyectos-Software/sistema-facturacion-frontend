
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public class LoginRequest
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
    [JsonPropertyName("username")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [JsonPropertyName("password")]
    public required string Password { get; set; }
}

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

}