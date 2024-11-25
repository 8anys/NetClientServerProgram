using System.Net;
using System.Net.Sockets;

namespace ServerApp
{
    class NetworkServer
    {
        private TcpListener _tcpListener;

        public NetworkServer(string hostAddress, int listeningPort)
        {
            _tcpListener = new TcpListener(IPAddress.Parse(hostAddress), listeningPort);
        }

        public void Launch()
        {
            try
            {
                _tcpListener.Start();
                Console.WriteLine("Server is now operational...\n");

                while (true)
                {
                    using (var clientSocket = _tcpListener.AcceptTcpClient())
                    {
                        var processor = new ClientProcessor(clientSocket);
                        processor.ProcessClient();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
