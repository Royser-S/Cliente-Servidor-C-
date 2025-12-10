namespace EXAMEN_T2.Model
{
    public class Libro
    {
        // AHORA ES STRING (por el char(4))
        public string? CodigoLibro { get; set; }
        public string? TituloLibro { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }

        // Relación con Editorial
        public string? CodigoEditorial { get; set; }
        public string? NombreEditorial { get; set; } // Para el JOIN
    }
}
