using Punto.objects;

namespace Punto.events
{
    public class OnMaxPlayerGame
    {
        public OnMaxPlayerGame(Game game)
        {
            this.Game = game;
            // Appeler la méthode Launch de manière asynchrone
            _ = LaunchGameAsync(); // Utiliser le _ pour ignorer le résultat de la tâche
        }

        public Game Game { get; set; }

        private async Task LaunchGameAsync()
        {
            await this.Game.Launch();
        }
    }
}