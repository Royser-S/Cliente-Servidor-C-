using EXAMEN_T2.Model;
using EXAMEN_T2.Repositorio.Interface;
using Microsoft.Data.SqlClient;

namespace EXAMEN_T2.Repositorio.DAO
{
    public class libroDAO : ILibro
    {
        private readonly string cadena;

        public libroDAO()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }

        public string deleteLibro(string id)
        {
            string msj = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarLibro", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoLibro", id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    msj = "Libro eliminado";
                }
                catch (SqlException ex) { msj = ex.Message; }
                finally { cn.Close(); }

            }
            return msj;
        }

        public Libro getLibro(string id)
        {
            return getLibros().FirstOrDefault(e => e.CodigoLibro == id);
        }

        public IEnumerable<Libro> getLibros()
        {
            List<Libro> lista = new List<Libro>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarLibros", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Libro()
                    {
                        CodigoLibro = dr.GetString(0),
                        TituloLibro = dr.GetString(1),
                        Autor = dr.GetString(2),
                        Genero = dr.GetString(3),
                        CodigoEditorial = dr.GetString(4),
                        NombreEditorial = dr.GetString(5)
                    });
                }
                dr.Close();

            }
            return lista;
        }

        public IEnumerable<Libro> getLibrosPorAutor(string autor)
        {
            List<Libro> lista = new List<Libro>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarLibrosPorAutor", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Autor", autor);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Libro()
                    {
                        CodigoLibro = dr.GetString(0),
                        TituloLibro = dr.GetString(1),
                        Autor = dr.GetString(2),
                        Genero = dr.GetString(3),
                        CodigoEditorial = dr.GetString(4),
                        NombreEditorial = dr.GetString(5)
                    });
                }
            }
            return lista;
        }

        public IEnumerable<Libro> getLibrosPorEditorial(string ideditorial)
        {
            List<Libro> lista = new List<Libro>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarLibrosPorEditorial", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodigoEditorial", ideditorial);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Libro()
                    {
                        CodigoLibro = dr.GetString(0),
                        TituloLibro = dr.GetString(1),
                        Autor = dr.GetString(2),
                        Genero = dr.GetString(3),
                        CodigoEditorial = dr.GetString(4),
                        NombreEditorial = dr.GetString(5)
                    });
                }
            }
            return lista;
        }

        public string insertLibro(Libro reg)
        {
            string msj = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarLibro", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // IMPORTANTE: Aquí enviamos el Código manual
                    cmd.Parameters.AddWithValue("@CodigoLibro", reg.CodigoLibro);
                    cmd.Parameters.AddWithValue("@TituloLibro", reg.TituloLibro);
                    cmd.Parameters.AddWithValue("@Autor", reg.Autor);
                    cmd.Parameters.AddWithValue("@Genero", reg.Genero);
                    cmd.Parameters.AddWithValue("@CodigoEditorial", reg.CodigoEditorial);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    msj = "Libro registrado correctamente";
                }
                catch (SqlException ex) { msj = ex.Message; }
                finally { cn.Close(); }

            }
            return msj;
        }

        public string updateLibro(Libro reg)
        {
            string msj = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarLibro", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoLibro", reg.CodigoLibro);
                    cmd.Parameters.AddWithValue("@TituloLibro", reg.TituloLibro);
                    cmd.Parameters.AddWithValue("@Autor", reg.Autor);
                    cmd.Parameters.AddWithValue("@Genero", reg.Genero);
                    cmd.Parameters.AddWithValue("@CodigoEditorial", reg.CodigoEditorial);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    msj = "Libro actualizado correctamente";
                }
                catch (SqlException ex) { msj = ex.Message; }
                finally { cn.Close(); }

            }
            return msj;
        }
    }
}
