using System;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    class ClientProcessor
    {
        private TcpClient _connection;
        private NetworkStream _dataStream;

        public ClientProcessor(TcpClient clientSocket)
        {
            _connection = clientSocket;
            _dataStream = _connection.GetStream();
        }

        public void ProcessClient()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = _dataStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string clientMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Client sent: {clientMessage}");

                    
                    switch (clientMessage.Trim().ToLower())
                    {
                        case "!quit":
                            Console.WriteLine("Client requested termination. Closing session...");
                            bytesRead = 0; 
                            break;

                        default:
                            Console.Write("Reply to Client: ");
                            string serverReply = Console.ReadLine();
                            byte[] replyData = Encoding.UTF8.GetBytes(serverReply);
                            _dataStream.Write(replyData, 0, replyData.Length);

                            switch (serverReply.Trim().ToLower())
                            {
                                case "!quit":
                                    Console.WriteLine("Shutting down...");
                                    bytesRead = 0; 
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error occurred: {error.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
