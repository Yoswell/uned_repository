/*== Inicio de un servidor en el equipo local ==*/
using namespace Uned.Temas;

using System;
using System.Net;
using System.Net.Sockets;

class Server {
    private TcpListener _listener;
    private string _host = "127.0.0.1";
    private int _port = 5000;

    /*== Lista de clientes ==*/
    private List<TcpClient> _clientes = new List<TcpClient>();

    public Server() {
        this._listener = new TcpListener(IPAddress.Parse(_host), _port);
    }

    public async Task Start() {
        try {
            _listener.Start();
            Console.WriteLine("El servidor se inicio correctamente");

            /*== Bucle principal para aceptar clientes ==*/
            while (true) {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                _clientes.Add(client);
                handleClientsAync();
            }
        } catch(Exception) { }
    }

    /*== Manejo de clientes conectados ==*/
    private void handleClientsAync() {
        Console.WriteLine($"Clientes conectados: {_clientes.Count()}");
    }
}