using System;
using System.Text;

namespace NetworkClient
{
    class Application
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("//-- CLIENT PROGRAM --\\\\" + "\n");

            Console.Write("Enter server IP: ");
            string serverIp = Console.ReadLine(); // Example: 127.0.0.1

            Console.Write("Enter server port: ");
            string serverPort = Console.ReadLine(); // Example: 8080

            try
            {
                var client = new ConnectionClient(serverIp, int.Parse(serverPort));

                client.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error encountered: {exception.Message}");
            }
        }
    }
}
