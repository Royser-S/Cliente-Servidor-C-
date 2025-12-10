using EXAMEN_T2.Model;

namespace EXAMEN_T2.Repositorio.Interface
{
    public interface ILibro
    {
        IEnumerable<Libro> getLibros();
        IEnumerable<Libro> getLibrosPorAutor(string autor);
        IEnumerable<Libro> getLibrosPorEditorial(string ideditorial); // Extra para el combo

        Libro getLibro(string id); // ID es string
        string insertLibro(Libro reg);
        string updateLibro(Libro reg);
        string deleteLibro(string id); // ID es string
    }
}
