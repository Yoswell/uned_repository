/*== Inicio de un servidor en el equipo local ==*/
using namespace Uned.Temas;

using System;
using System.Net;
using System.Net.Sockets;

class Server {
    private TcpListener _listener;

    /*== Configuración de conexión TCP ==*/
    private string _host = "127.0.0.1";
    private int _port = 5000;

    public Server() {
        this._listener = new TcpListener(IPAddress.Parse(_host), _port);
    }

    public void start() {
        try {
            _listener.Start();
            Console.WriteLine("El servidor se inicio correctamente");
        } catch(Exception) { }
    }
}