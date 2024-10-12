using System.Net;
using System.Net.Sockets;
using System.Text;

// Порт для прослушивания сообщений
const int LISTEN_PORT = 13400;
// Порт для отправления сообщений
const int SEND_PORT = 13400;

// Сокет для работы по сети
using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

// Точка для прослушивания сообщений
IPEndPoint ipListen = new IPEndPoint(IPAddress.Any, LISTEN_PORT);

// Начинаем слушать входящие сообщения
socket.Bind(ipListen);

// Метод для прослушивания
static async Task ListenMessage(Socket socket, IPEndPoint ipListen)
{
    while (true)
    {
        // Данные для чтения
        byte[] data = new byte[256];
        // Получаем данные от клиента
        await socket.ReceiveFromAsync(data, ipListen);

        Console.WriteLine(Encoding.Unicode.GetString(data).Replace("\0", string.Empty));

        string answer = "Получил!";
        Console.WriteLine($"Ip user: {socket.RemoteEndPoint}");
        await socket.SendToAsync(Encoding.Unicode.GetBytes(answer), socket.RemoteEndPoint);
    }
}

await Task.Factory.StartNew(() => ListenMessage(socket, ipListen));