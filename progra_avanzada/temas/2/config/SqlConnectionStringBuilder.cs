/*== Construccion de una cadena de conexion usando SqlClient ==*/
using namespace Uned.Temas;
using Microsoft.Data.SqlClient;

class SqlCommand {
    static void Main(string[] args) {
        var builder = new SqlConnectionStringBuilder();
        builder.DataSource = "DESKTOP-T5BBINC";
        builder.InitialCatalog = "PcGamersDB";
        builder.IntegratedSecurity = true;  // Windows Auth
        builder.TrustServerCertificate = true;
        builder.ConnectTimeout = 30;

        string connectionString = builder.ConnectionString;
        Console.WriteLine(connectionString);
    }
}