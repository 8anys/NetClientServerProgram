using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    public class Application
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("\\-- SERVER APPLICATION --\\\n");

            Console.Write("Enter server IP: ");
            string serverIp = Console.ReadLine();

            Console.Write("Enter server port: ");
            if (!int.TryParse(Console.ReadLine(), out int serverPort))
            {
                Console.WriteLine("Invalid port number");
                return;
            }

            var server = new TcpListener(System.Net.IPAddress.Parse(serverIp), serverPort);
            try
            {
                server.Start();
                Console.WriteLine("Server is up and running...\n");

                while (true)
                {
                    var client = server.AcceptTcpClient();
                    var session = new ClientHandler(client);
                    session.HandleSession();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server error: {ex.Message}");
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
