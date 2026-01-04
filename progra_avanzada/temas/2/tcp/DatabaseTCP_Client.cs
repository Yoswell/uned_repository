/*== Envio de consultas sql al servidor ==*/
using namespace Uned.Temas;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client {
    private TcpClient _client;
    private string _host = "127.0.0.1";
    private int _port = 5000;

    public Client() {
        this._client = new TcpClient();
    }

    /*== Conexión del cliente y envío de consulta SQL ==*/
    public async Task start() {
        try {
            await _client.ConnectAsync(IPAddress.Parse(_host), _port);
            Console.WriteLine("El cliente se inicio correctamente");

            using(NetworkStream stream = _client.GetStream()) {
                byte[] data = Encoding.ASCII.GetBytes("SELECT * FROM Clientes");
                stream.Write(data);
            }
        } catch(Exception) { }
    }
}