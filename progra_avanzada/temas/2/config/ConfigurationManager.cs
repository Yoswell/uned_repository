/*== Obtencion de la cadena de coneccion desde los archivos config ==*/
using namespace Uned.Temas;
using System.Configuration;

class SqlCommand {
    static void Main(string[] args) {
        /*== Obtener la cadena de conexion ==*/
        var connectionString2 = ConfigurationManager.ConnectionStrings['MiConnectionSql'].ConnectionString;
    }
}