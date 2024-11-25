using System.Text;

namespace ServerApp
{
    class Application
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("//-- SERVER APPLICATION --\\\\" + "\n");

            Console.Write("Enter server IP address: ");
            string serverAddress = Console.ReadLine(); // e.g., 127.0.0.1
            Console.Write("Enter server port: ");
            string serverPort = Console.ReadLine(); // e.g., 8080

            var networkServer = new NetworkServer(serverAddress, int.Parse(serverPort));
            networkServer.Launch();
        }
    }
}
