using EXAMEN_T2.Model;
using EXAMEN_T2.Repositorio.Interface;
using Microsoft.Data.SqlClient;

namespace EXAMEN_T2.Repositorio.DAO
{
    public class paisDAO : IPais
    {

        private readonly string cadena;

        public paisDAO()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }


        public IEnumerable<Pais> getPaises()
        {
            List<Pais> temporal = new List<Pais>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                // Usamos TU nombre de SP
                SqlCommand cmd = new SqlCommand("sp_ListarPais", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Pais()
                    {
                        CodigoPais = dr.GetString(0),
                        NombrePais = dr.GetString(1)
                    });
                }
            }
            return temporal;
        }


    }
}
