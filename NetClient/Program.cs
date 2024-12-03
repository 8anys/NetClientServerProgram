using System;
using System.Text;

namespace NetworkClient
{
    class Application
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("\\-- CLIENT APPLICATION --\\\n");

            Console.Write("Enter server IP: ");
            string ip = Console.ReadLine();

            Console.Write("Enter server port: ");
            if (!int.TryParse(Console.ReadLine(), out int port))
            {
                Console.WriteLine("Invalid port number");
                return;
            }

            try
            {
                var client = new ConnectionClient(ip, port);
                client.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
