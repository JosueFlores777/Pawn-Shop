using System.Data.SqlClient;

namespace pawnShop.Data
{
    public class Conexion
    {

        private string stringSql = string.Empty;
        public Conexion() { 
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            stringSql = builder.GetSection("ConnectionStrings:Connection").Value;
        }

        public string getConexion() { 
        
            return stringSql;
        }
    }
}
