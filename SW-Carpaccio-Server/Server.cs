using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SW_Carpaccio_Server
{
    public class Server
    {
        public static void Start()
        {
            int port = 69;

            TcpListener serverSocket = new TcpListener(IPAddress.Any, port);
            serverSocket.Start();
            Console.WriteLine("Server activated");

            TcpClient client = serverSocket.AcceptTcpClient();
            Console.WriteLine("Client connected");

            using (client)
            {
                NetworkStream ns = client.GetStream();
                StreamReader reader = new StreamReader(ns);
                StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

                string inputLine = "";
                while (inputLine != null)
                {
                    // Amount of items.
                    inputLine = reader.ReadLine();
                    //writer.WriteLine($"input {inputLine}");
                    Console.WriteLine($"input {inputLine}");
                    int AmountOfItems = Convert.ToInt32(inputLine);

                    // Price
                    inputLine = reader.ReadLine();
                    //writer.WriteLine($"input {inputLine}");
                    Console.WriteLine($"input {inputLine}");
                    double PriceOfItem = Convert.ToDouble(inputLine);

                    // State code
                    inputLine = reader.ReadLine();
                    //writer.WriteLine($"input {inputLine}");
                    Console.WriteLine($"input {inputLine}");
                    string stateCode = inputLine;

                    // Calculations
                    double taxPercentage = Calculator.GetTax(stateCode);
                    double totalWithDiscount = Calculator.ResultDiscount(AmountOfItems, PriceOfItem);
                    double totalAmount = totalWithDiscount * taxPercentage;

                    //Outputs total
                    writer.WriteLine($"The total amount is = {totalAmount}");
                    Console.WriteLine($"The total amount is = {totalAmount}");
                }
            }
        }
    }

    public class Calculator
    {
        /// <summary>
        /// Gets the total price without discount and taxes
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static double ResultNoTax(int amount, double price)
        {
            return amount * price;
        }

        /// <summary>
        /// Gets the total price with discount but without taxes.
        /// Inside the function theres a switch statement that calculates the discount based on the price
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static double ResultDiscount(int amount, double price)
        {

            double result = ResultNoTax(amount, price);


            switch (result)
            {
                case var n when (n < 1000):
                    return result;
                case var n when (n <= 1000 && n < 5000):
                    return result * 0.97;
                case var n when (n <= 5000 && n < 7000):
                    return result * 0.95;
                case var n when (n <= 7000 && n < 10000):
                    return result * 0.93;
                case var n when (n >= 10000 && n < 50000):
                    return result * 0.90;
                case var n when (n >= 50000):
                    return result * 0.85;
                default:
                    return result;
            }
        }
        public static double GetTax(string stateCode)
        {
            double tax = 0;

            switch (stateCode.ToUpper())
            {
                case "UT":
                    tax = 6.85;
                    break;
                case "NV":
                    tax = 8.00;
                    break;
                case "TX":
                    tax = 6.25;
                    break;
                case "AL":
                    tax = 4.00;
                    break;
                case "CA":
                    tax = 8.25;
                    break;
                default:
                    return 1;
            }

            return (tax / 100) + 1;
        }
    }
}