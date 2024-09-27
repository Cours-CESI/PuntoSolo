using Punto;
using Punto.objects;

class hub
{
    static void Main(string[] args)
    {
        WebSocketServer.StartWebSocketServerAsync().Wait();
        return;
        Console.WriteLine("Punto Client Start");
        
        Game game = new Game(2);
        ServerPlayer player1 = new ServerPlayer("Player1");
        ServerPlayer player2 = new ServerPlayer("Player2");
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        
    }
}