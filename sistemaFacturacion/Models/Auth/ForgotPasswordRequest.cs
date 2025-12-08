using System.ComponentModel.DataAnnotations;

namespace sistemaFacturacion;

public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo válido.")]
    [MaxLength(150, ErrorMessage = "El correo debe tener máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;
}
