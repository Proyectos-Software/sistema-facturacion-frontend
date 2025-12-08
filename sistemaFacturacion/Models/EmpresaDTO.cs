using System;
using System.ComponentModel.DataAnnotations;

namespace sistemaFacturacion.Models
{
    // DTO principal que devuelve el GET
    public class EmpresaDto
    {
        public int IdEmp { get; set; }
        public string NomEmp { get; set; } = string.Empty;
        public string RucEmp { get; set; } = string.Empty;
        public string DirEmp { get; set; } = string.Empty;
        public string TelEmp { get; set; } = string.Empty;
        public string CorEmp { get; set; } = string.Empty;
        public string AmbEmp { get; set; } = string.Empty;

        // Configuración SRI
        public string Establecimiento { get; set; } = string.Empty;
        public string PuntoEmision { get; set; } = string.Empty;
        public string CodigoNumerico { get; set; } = string.Empty;
        public string UrlRecepcionSRI { get; set; } = string.Empty;
        public string UrlAutorizacionSRI { get; set; } = string.Empty;

        // Certificado Digital
        public bool TieneCertificado { get; set; }
        public string? RutaCertificado { get; set; }
        public bool CertificadoExiste { get; set; }
        public DateTime? FechaVencimientoCertificado { get; set; }

        public bool EstEmp { get; set; }
    }

    // Request para actualizar configuración básica
    public class EmpresaConfigBasicaRequest
    {
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string NomEmp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RUC es obligatorio.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "El RUC debe tener 13 dígitos.")]
        public string RucEmp { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
        public string DirEmp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        public string TelEmp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        public string CorEmp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ambiente es obligatorio.")]
        public string AmbEmp { get; set; } = string.Empty;
    }

    // Request para configurar parámetros del SRI
    public class EmpresaConfigSriRequest
    {
        [Required(ErrorMessage = "El establecimiento es obligatorio.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El establecimiento debe tener 3 dígitos.")]
        public string Establecimiento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El punto de emisión es obligatorio.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El punto de emisión debe tener 3 dígitos.")]
        public string PuntoEmision { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código numérico es obligatorio.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El código numérico debe tener 8 dígitos.")]
        public string CodigoNumerico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ambiente es obligatorio.")]
        public string AmbEmp { get; set; } = string.Empty;
    }

    // Request para subir certificado digital
    public class CertificadoUploadRequest
    {
        public byte[] ArchivoBytes { get; set; } = Array.Empty<byte>();

        public string NombreArchivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña del certificado es obligatoria.")]
        [MinLength(1, ErrorMessage = "La contraseña no puede estar vacía.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Fecha de Vencimiento del certificado es obligatoria.")]
        [MinLength(1, ErrorMessage = "La Fecha de Vencimiento no puede estar vacía.")]
        public DateTime? FechaVencimiento { get; set; }

    }

    // Response del estado del certificado
    public class CertificadoEstadoDto
    {
        public bool Existe { get; set; }
        public string? RutaCertificado { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; }
        public int DiasRestantes { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}