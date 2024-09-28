using Microsoft.AspNetCore.SignalR;
using Punto.events;

namespace Punto.objects;

public class Game
{
    private readonly IHubContext<ChatHub> _hubContext;
    public Board Board;
    private int MaxPlayers { get; set; }
    private List<ServerPlayer> Players = new List<ServerPlayer>();
    public GameStats Stat { get; set; }
    public bool isFirstTurn = true;
    public int GameLoop;

    
    public Game(IHubContext<ChatHub> hubContext, int maxPlayers)
    {
        this.GameLoop = 0;
        this._hubContext = hubContext;
        this.Board = new Board();
        this.MaxPlayers = maxPlayers;
    }

    /// Ajoute un joueur à la game courante
    /// <param name="player"><c>String</c> Nom du joueur qui a rejoint</param>
    public async Task AddPlayer(ServerPlayer player)
    {
        if (this.Players.Count < MaxPlayers)
        {
            this.Players.Add(player);
            Console.WriteLine($"{player.Name} a rejoint la partie.");
            if (this.Players.Count == MaxPlayers) new OnMaxPlayerGame(this);
        }
        else
        {
            Console.WriteLine("La partie est pleine !");
        }
    }

    /// Lances the game and starts the main game loop.
    /// Sets the game status to "InGame" and handles player turns until the game ends.
    /// Outputs game status information to the console.
    /// When the game loop exceeds 16 iterations, the game status is set to "Finished".
    /// <return>A task that represents the asynchronous operation.</return>
    public async Task Launch()
    {
        Console.WriteLine("Game Start");
        this.Stat = GameStats.InGame;

        this.GameLoop = 1;
        int offset = this.Board.GridSize / 2; // Ajoute le décalage ici

        while (this.Stat == GameStats.InGame)
        {
            Console.WriteLine($"Game Loop {this.GameLoop}");
    
            foreach (ServerPlayer player in this.Players)
            {
                Tuile usedTuile = player.Turn(this);
            
                // Ajoute l'offset ici lors de l'accès à la case
                Case currentCase = this.Board.Tray[usedTuile.X + offset][usedTuile.Y + offset]; // Utilisation de l'offset
                currentCase.Tuiles.Push(usedTuile);
            }
            await this.Board.SendTray(this._hubContext, this);
            Console.ReadLine();
            // this.Board.Write();
            this.GameLoop++;
            if (this.GameLoop > 18) this.Stat = GameStats.Finished;
        }
    }

}