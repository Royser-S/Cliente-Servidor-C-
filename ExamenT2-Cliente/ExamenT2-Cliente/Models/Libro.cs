using System.ComponentModel.DataAnnotations;

namespace ExamenT2_Cliente.Models
{
    public class Libro
    {
        [Required]
        [Display(Name = "Cód. Libro")]
        public string? CodigoLibro { get; set; } // Es char(4), el usuario lo escribe

        [Required]
        [Display(Name = "Título del Libro")]
        public string? TituloLibro { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public string? Autor { get; set; }

        [Required]
        [Display(Name = "Género")]
        public string? Genero { get; set; }

        [Required]
        [Display(Name = "Editorial")]
        public string? CodigoEditorial { get; set; } // Para guardar el valor del Combo

        [Display(Name = "Editorial")]
        public string? NombreEditorial { get; set; } // Para mostrar en la tabla (JOIN)
    }
}
