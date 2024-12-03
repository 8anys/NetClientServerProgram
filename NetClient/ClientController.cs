using System;
using System.Net.Sockets;
using System.Text;

namespace NetworkClient
{
    public class ConnectionClient
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public ConnectionClient(string serverIp, int serverPort)
        {
            _client = new TcpClient(serverIp, serverPort);
            _stream = _client.GetStream();
            Console.WriteLine($"Connected to server at {serverIp}:{serverPort}");
        }

        public void Execute()
        {
            try
            {
                while (true)
                {
                    Console.Write("Enter message: ");
                    string message = Console.ReadLine();

                    if (string.Equals(message, "exit", StringComparison.OrdinalIgnoreCase))
                        break;

                    var buffer = Encoding.UTF8.GetBytes(message);
                    _stream.Write(buffer, 0, buffer.Length);

                    var responseBuffer = new byte[1024];
                    int bytesRead = _stream.Read(responseBuffer, 0, responseBuffer.Length);
                    var serverReply = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                    Console.WriteLine($"Server response: {serverReply}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in communication: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        private void CloseConnection()
        {
            _stream?.Close();
            _client?.Close();
            Console.WriteLine("Connection closed.");
        }
    }
}