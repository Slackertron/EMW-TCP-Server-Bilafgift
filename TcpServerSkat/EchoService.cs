using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TcpServerSkat
{
    class EchoService
    {
        private TcpClient connectionSocket;

        public EchoService(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }
        internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            sw.WriteLine("Er det Personbil eller Elbil, som du vil finde afgiften på ?");
            string message = "Startup";
            string answer;
            while (!String.IsNullOrEmpty(message))
            {
                message = sr.ReadLine();
                Console.WriteLine("Client: " + message);

                // Tager parameteret for biltypen.
                string parameter2 = message;
                sw.WriteLine("Indsæt Pris:");

                // Tager parameteret for pris på bilen.
                message = sr.ReadLine();
                Console.WriteLine("Client: " + message);
                double parameter1 = Convert.ToDouble(message);

                // Kører metoden fra Skat.Afgift ved navn BilAfgift, metoden tager to parameter: pris og type.
                //Derefter bliver resultatet fra metoden konverteret til string, for at blive sendt til client.
                answer = Convert.ToString(Skat.Afgift.BilAfgift(parameter1, parameter2));
                sw.WriteLine(answer);
                sw.WriteLine("Er det Personbil eller Elbil, som du vil finde afgiften på? Ellers tryk enter for at afslutte.");
            }
            ns.Close();
            connectionSocket.Close();
        }
    }
}
