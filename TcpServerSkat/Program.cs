using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TcpServerSkat
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 6789);

            serverSocket.Start();

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated now");
                EchoService service = new EchoService(connectionSocket);

                Task.Factory.StartNew(() => service.DoIt());
            }
            serverSocket.Stop();
        }
    }

}