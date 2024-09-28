using Punto.objects;

namespace Punto;

public class Utils
{
    public static Stack<T> ShuffleStack<T>(Stack<T> stack)
    {
        // Convertir la pile en liste
        List<T> list = stack.ToList();

        // Utiliser LINQ pour mélanger la liste
        Random rng = new Random();
        list = list.OrderBy(a => rng.Next()).ToList();

        // Recréer la pile avec la liste mélangée
        return new Stack<T>(list);
    }
    
    public static void LogInvalidMove(Tuile tile, string reason)
    {
        Console.WriteLine($"Mouvement invalide pour la tuile {tile.Number} à ({tile.X}, {tile.Y}). Raison: {reason}");
    }
}