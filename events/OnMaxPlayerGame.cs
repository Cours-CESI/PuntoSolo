using Punto.objects;

namespace Punto.events;

public class OnMaxPlayerGame
{
    public OnMaxPlayerGame(Game game)
    {
        this.Game = game;
        this.Game.Launch();
    }

    public Game Game { get; set; }
}