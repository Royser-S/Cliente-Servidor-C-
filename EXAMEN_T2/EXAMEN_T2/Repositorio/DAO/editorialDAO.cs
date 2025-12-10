using EXAMEN_T2.Model;
using EXAMEN_T2.Repositorio.Interface;
using Microsoft.Data.SqlClient;

namespace EXAMEN_T2.Repositorio.DAO
{
    public class editorialDAO : IEditorial
    {

        private readonly string cadena;

        public editorialDAO()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }

        public IEnumerable<Editorial> getEditoriales()
        {
            List<Editorial> temporal = new List<Editorial>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarEditoriales", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Editorial()
                    {
                        CodigoEditorial = dr.GetString(0),
                        NombreEditorial = dr.GetString(1),
                        Direccion = dr.GetString(2),
                        Email = dr.GetString(3),
                        CodigoPais = dr.GetString(4),
                        NombrePais = dr.GetString(5)
                    });
                }
                dr.Close();
            }
            return temporal;
        }

        // Método auxiliar para buscar una sola editorial (necesario para el EDITAR)
        public Editorial getEditorial(string id)
        {
            // Como no pasaste SP de buscar por ID, usaremos LINQ sobre la lista
            // (Es válido para examen si no te piden SP específico de búsqueda por ID)
            return getEditoriales().FirstOrDefault(e => e.CodigoEditorial == id);
        }

        public string insertEditorial(Editorial reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarEditorial", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoEditorial", reg.CodigoEditorial);
                    cmd.Parameters.AddWithValue("@NombreEditorial", reg.NombreEditorial);
                    cmd.Parameters.AddWithValue("@Direccion", reg.Direccion);
                    cmd.Parameters.AddWithValue("@Email", reg.Email);
                    cmd.Parameters.AddWithValue("@CodigoPais", reg.CodigoPais);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Editorial registrada correctamente";
                }
                catch (SqlException ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string updateEditorial(Editorial reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    // OJO: Usé tu nombre exacto "sp_ActualizarEditoral" (sin la i al final)
                    SqlCommand cmd = new SqlCommand("sp_ActualizarEditoral", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoEditorial", reg.CodigoEditorial);
                    cmd.Parameters.AddWithValue("@NombreEditorial", reg.NombreEditorial);
                    cmd.Parameters.AddWithValue("@Direccion", reg.Direccion);
                    cmd.Parameters.AddWithValue("@Email", reg.Email);
                    cmd.Parameters.AddWithValue("@CodigoPais", reg.CodigoPais);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Editorial actualizada correctamente";
                }
                catch (SqlException ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string deleteEditorial(string id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarEditorial", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoEditorial", id);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Editorial eliminada correctamente";
                }
                catch (SqlException ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public IEnumerable<Editorial> getEditorialesPorNombre(string nombre)
        {
            List<Editorial> temporal = new List<Editorial>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_BuscarEditorialPorNombre", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Editorial()
                    {
                        CodigoEditorial = dr.GetString(0),
                        NombreEditorial = dr.GetString(1),
                        Direccion = dr.GetString(2),
                        Email = dr.GetString(3),
                        CodigoPais = dr.GetString(4),
                        NombrePais = dr.GetString(5)
                    });
                }
            }
            return temporal;
        }

    }
}
