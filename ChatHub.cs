using Microsoft.AspNetCore.SignalR;
using Punto.objects;

namespace Punto;

public class ChatHub : Hub
{
    private readonly Game _game;

    /// Classe représentant le hub de chat pour la communication en temps réel.
    public ChatHub(Game game)
    {
        _game = game;
    }
    
    /**
     * Fonction permettant d'ajouter un joueur à la partie courante
     * <param name="playerName"><c>String</c> Désigne le nom du joueur</param>
     */
    public async Task JoinGame(string playerName)
    {
        ServerPlayer player = new ServerPlayer(playerName);
        await _game.AddPlayer(player);
    }
    
    /**
     * Fonction permettant d'envoyer la taille de la grill
     */
    public async Task<int> GetGridSize()
    {
        Console.WriteLine("Get grid size " + this._game.Board.GridSize);
        return this._game.Board.GridSize;
    }
    
}