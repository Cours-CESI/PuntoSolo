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
}