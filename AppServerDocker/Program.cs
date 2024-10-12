using System.Net;
using System.Net.Sockets;
using System.Text;

UdpClient udpListener = new UdpClient(8000);

// Сервер прослушивает
while (true)
{
    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    byte[] receivedBytes = udpListener.Receive(ref remoteEndPoint);

    string result = Encoding.UTF8.GetString(receivedBytes);

    string ip = remoteEndPoint.Address.ToString();
    Console.WriteLine($"Your ip: {ip}\nData: {result}");

    udpListener.Send(Encoding.Unicode.GetBytes(ip), remoteEndPoint);
}