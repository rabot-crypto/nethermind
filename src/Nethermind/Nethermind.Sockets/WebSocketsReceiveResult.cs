using System.Net.WebSockets;

namespace Nethermind.Sockets
{
    public class WebSocketsReceiveResult : ReceiveResult
    {
        public WebSocketCloseStatus? CloseStatus { get; set; }
    }
}
