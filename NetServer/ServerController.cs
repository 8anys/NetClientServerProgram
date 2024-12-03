using System.Net;
using System.Net.Sockets;

namespace ServerApp
{
    public class NetworkServer
    {
        private TcpListener _tcpListener;

        public NetworkServer(string hostAddress, int listeningPort)
        {
            _tcpListener = new TcpListener(IPAddress.Parse(hostAddress), listeningPort);
        }

        public void Start()
        {
            _tcpListener.Start();
        }

        public TcpClient AcceptTcpClient()
        {
            return _tcpListener.AcceptTcpClient();
        }

        public void Stop()
        {
            _tcpListener.Stop();
        }
    }
}
