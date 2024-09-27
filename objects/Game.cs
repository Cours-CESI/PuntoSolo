using Punto.events;

namespace Punto.objects;

public class Game
{
    private Board Board;
    private int MaxPlayers { get; set; }
    private List<ServerPlayer> Players = new List<ServerPlayer>();
    public GameStats Stat { get; set; }

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
        this.Stat = GameStats.InGame;
        foreach (ServerPlayer player in this.Players)
        {
            Console.WriteLine($"Player {player.Name} :");
            Console.WriteLine($"Tuiles : {player.Tuiles.Count}");
            Console.WriteLine($"Main : {player.TuilesMain.Count}");
        }

        int gameLoop = 1;
        while (this.Stat == GameStats.InGame)
        {
            Console.WriteLine($"Game Loop {gameLoop}");

            foreach (ServerPlayer player in this.Players)
            {
                Tuile usedTuile = player.Turn(this.Board);
                
                Case currentCase = this.Board.Tray[usedTuile.X][usedTuile.Y];
                currentCase.Tuiles.Push(usedTuile);
            }

            foreach (List<Case> col in this.Board.Tray)
            {
                foreach (Case cell in col)
                {
                    if (cell.Tuiles.Count == 0)
                    {
                        Console.Write($"[ N ] ");
                    }
                    else
                    {
                        Console.Write("[" + cell.Tuiles.Peek().Number + " / " + cell.Tuiles.Peek().SPlayer.Name + "] ");
                    }
                }
                Console.WriteLine();
            }
            
            gameLoop++;
            if(gameLoop > 16) this.Stat = GameStats.Finished;
        }
    }

}