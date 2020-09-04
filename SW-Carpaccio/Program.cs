using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;

namespace SW_Carpaccio
{
    public class Program
    {
        //1: Bruger skal kunne inputte en mængde
        //2: bruger skal kunne inputte en pris
        //3: Bruger skal kunne inputte en to-bogstavs stat kode
        //4: program skal regne den totale pris ud, uden discount og uden skat
        //5: program skal kunne finde ud af hvilken discount rate der skal bruges
        //6: program skal kunne trække rabatten fra
        //7: program skal kunne finde procenten på skatten ud fra statskoden
        //8: program skal ligge statsskat oveni prisen
        //9: Bruger skal kunne se resultat i kr/$
        //10: program skal være klar til næste input

        static void Main()
        {
            StartClient();
        }

        public static void StartClient()
        {
            int port = 69;

            TcpClient client = new TcpClient("localhost", port);

            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            while (true)
            {
                // Amount of items
                Console.WriteLine("Input your amount of items:");
                string AmountOfItems = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine($"Sending {AmountOfItems} to server");
                Console.WriteLine();
                writer.WriteLine(AmountOfItems);
                //string AmountReceived = reader.ReadLine();
                //Console.WriteLine($"Received {lineReceived} from server");
                //Console.WriteLine();

                //Price
                Console.WriteLine("Input the price:");
                string Price = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine($"Sending {Price} to server");
                Console.WriteLine();
                writer.WriteLine(Price);
                //string lineReceived = reader.ReadLine();
                //Console.WriteLine($"Received {lineReceived} from server");
                //Console.WriteLine();

                //State code
                Console.WriteLine("Input 2-letter state code:");
                string stateCode = Console.ReadLine();
                if (stateCode.Length != 2)
                {
                    Console.WriteLine("Input 2-letter state code:");
                    stateCode = Console.ReadLine();
                }
                else
                {
                    string line = stateCode;
                    Console.WriteLine();
                    Console.WriteLine($"Sending {line} to server");
                    Console.WriteLine();
                    writer.WriteLine(line);
                }

                //Total
                string lineReceived = reader.ReadLine();
                Console.WriteLine($"Received {lineReceived} from server");
                Console.WriteLine();
            }
        }

    }
}
