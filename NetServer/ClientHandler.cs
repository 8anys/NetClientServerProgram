using System;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    public class ClientHandler
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public ClientHandler(TcpClient client)
        {
            _client = client;
            _stream = _client.GetStream();
        }

        public void HandleSession()
        {
            try
            {
                var buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = _stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Message from client: {receivedMessage}");

                    if (receivedMessage.Trim().Equals("!quit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Client has requested to end the session.");
                        break;
                    }

                    Console.Write("Server response: ");
                    string response = Console.ReadLine();
                    var responseData = Encoding.UTF8.GetBytes(response);
                    _stream.Write(responseData, 0, responseData.Length);

                    if (response.Trim().Equals("!quit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Server is terminating the session.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Session error: {ex.Message}");
            }
            finally
            {
                _client?.Close();
            }
        }
    }
}