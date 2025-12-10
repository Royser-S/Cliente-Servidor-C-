using EXAMEN_T2.Model;

namespace EXAMEN_T2.Repositorio.Interface
{
    public interface IEditorial
    {
        IEnumerable<Editorial> getEditoriales();
        IEnumerable<Editorial> getEditorialesPorNombre(string nombre);
        // El buscar ahora recibe string
        Editorial getEditorial(string id);
        string insertEditorial(Editorial reg);
        string updateEditorial(Editorial reg);
        string deleteEditorial(string id);
    }
}
