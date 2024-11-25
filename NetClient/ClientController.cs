using System;
using System.Net.Sockets;
using System.Text;

namespace NetworkClient
{
    public class ConnectionClient
    {
        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _networkStream;

        public ConnectionClient(string serverAddress, int serverPort)
        {
            _tcpClient = new TcpClient(serverAddress, serverPort);
            _networkStream = _tcpClient.GetStream();
            Console.WriteLine($"Successfully connected to the server at {serverAddress}:{serverPort}");
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    Console.Write("Message to send: ");
                    string inputMessage = Console.ReadLine();

                    if (inputMessage.Equals("exit", StringComparison.OrdinalIgnoreCase))
                        break;

                    byte[] outgoingData = Encoding.UTF8.GetBytes(inputMessage);
                    _networkStream.Write(outgoingData, 0, outgoingData.Length);

                    byte[] buffer = new byte[1024];
                    int receivedBytes = _networkStream.Read(buffer, 0, buffer.Length);
                    string serverResponse = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                    Console.WriteLine($"Response from server: {serverResponse}");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Communication error: {error.Message}");
            }
            finally
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            _networkStream?.Close();
            _tcpClient?.Close();
            Console.WriteLine("Disconnected from server.");
        }
    }
}
