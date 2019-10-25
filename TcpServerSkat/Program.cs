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
            //Etablere listeneren/socket med ip og portnummer
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 6789);

            //Starter listener
            serverSocket.Start();

            while (true)
            {
                //Sætter listener til at acceptere connection.
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated now");
                // laver et object af klassen EchoService
                EchoService service = new EchoService(connectionSocket);
                //Kører task på service.DoIt(), så serveren er concurrent.
                Task.Factory.StartNew(() => service.DoIt());
            }
            serverSocket.Stop();
        }
    }

}