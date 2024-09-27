namespace Punto;

using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

class WebSocketServer
{
    public static async Task StartWebSocketServerAsync()
    {
        HttpListener httpListener = new HttpListener();
        httpListener.Prefixes.Add("http://localhost:8080/");
        httpListener.Start();
        Console.WriteLine("WebSocket server started.");

        while (true)
        {
            HttpListenerContext context = await httpListener.GetContextAsync();

            if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                WebSocket webSocket = webSocketContext.WebSocket;
                
                Console.WriteLine("Connected!");

                // Sending a message from C# server to the client
                while (true)
                {
                    string message = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }

                // Close the WebSocket connection
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }
    }
}
