using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Skat;

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
            //Laver stream på connectionen og nye objecter af write/read
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            //sw.WriteLine skriver til Clienten.
            sw.WriteLine("Er det Personbil eller Elbil, som du vil finde afgiften på?");
            string message = "Startup";
            string answer;
            //Loopet er sat op, så man kan kører flere bil i træk i stedet for, at skulle lukke ned efter hver udregning.
            while (!String.IsNullOrEmpty(message))
            {
                // Tager parameteret for biltypen.
                message = sr.ReadLine();
                Console.WriteLine("Client: " + message);
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
