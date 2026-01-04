/*== Ejecucion de consultas sql en el servidor enviadas por el cliente ==*/
using namespace Uned.Temas;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Data.SqlClient;

class Server {
    private TcpListener _listener;
    private string _host = "127.0.0.1";
    private int _port = 5000;
    
    /*== Cadena de conexión a base de datos ==*/
    private string _connectionString = "Server=DESKTOP-T5BBINC;Database=PcGamersDB;TrustServerCertificate=True;Integrated Security=True;";

    public Server() {
        this._listener = new TcpListener(IPAddress.Parse(_host), _port);
    }

    /*== Inicialización del servidor y recepción de consultas ==*/
    public async Task Start() {
        try {
            _listener.Start();
            Console.WriteLine("El servidor se inicio correctamente");

            TcpClient client = await _listener.AcceptTcpClientAsync();
            using(NetworkStream stream = client.GetStream()) {
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string mensaje = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                
                /*== Ejecutar consulta SQL recibida ==*/
                ExecuteSqlQuery(mensaje);
            }
            
        } catch(Exception) { }
    }

    /*== Ejecución de consultas SQL en la base de datos ==*/
    private void ExecuteSqlQuery(string query) {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using(SqlCommand command = new SqlCommand(query, connection)) {
            using(SqlDataReader reader = command.ExecuteReader()) {
                List<object> nombres = new();

                /*== Lectura de resultados de la consulta ==*/
                while(reader.Read()) {
                    nombres.Add(reader["nombre"]);
                }

                foreach(var nombre in nombres) Console.WriteLine(nombre);
            }
        }

        connection.Close();
    }
}