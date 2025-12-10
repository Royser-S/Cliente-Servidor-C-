using System.ComponentModel.DataAnnotations;

namespace ExamenT2_Cliente.Models
{
    public class Editorial
    {
        [Required]
        [Display(Name = "Cód. Editorial")]
        public string? CodigoEditorial { get; set; }
        [Required]
        [Display(Name = "Nombre Editorial")]
        public string? NombreEditorial { get; set; }
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "País")]
        public string? CodigoPais { get; set; }
        [Display(Name = "País")]
        public string? NombrePais { get; set; }
    }
}
