/*== Obtencion de la cadena de coneccion desde los archivos config (JSON) ==*/
using namespace Uned.Temas;
using Microsoft.Extensions.Configuration;

class SqlCommand {
    static void Main(string[] args) {
        private static IConfiguration _configuration;

        /*== Obtener la cadena de conexion ==*/
        string connectionString1 = _configuration.GetConnectionString("MiConnectionSql");
    }
}