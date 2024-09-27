using Punto.events;

namespace Punto.objects;

public class Game
{
    private Board Board;
    private int MaxPlayers { get; set; }
    private List<ServerPlayer> Players = new List<ServerPlayer>();

    public Game (int maxPlayers)
    {
        this.Board = new Board();
        this.MaxPlayers = maxPlayers;
    }

    /// Adds a player to the game.
    /// <param name="player">The player to be added to the game.</param>
    /// /
    public void AddPlayer(ServerPlayer player)
    {
        if (this.Players.Count < MaxPlayers)
        {
            this.Players.Add(player);
            Console.WriteLine($"{player.Name} a rejoint la partie.");

            if (this.Players.Count == MaxPlayers)
            {
                new OnMaxPlayerGame(this); // Déclenche l'événement
            }
        }
        else
        {
            Console.WriteLine("La partie est pleine !");
        }
    }

    public void Launch()
    {
        Console.WriteLine("Game Start");
        foreach (ServerPlayer player in this.Players)
        {
            Console.WriteLine($"Player {player.Name} :");
            Console.WriteLine($"Tuiles : {player.Tuiles.Count}");
            Console.WriteLine($"Main : {player.TuilesMain.Count}");
        }
    }
}