namespace Punto.objects;

public class Case
{
    public int X { get; set; }
    public int Y { get; set; }
    public Stack<Tuile> Tuiles { get; set; }
    
    public Case(int x, int y)
    {
        this.X = x;
        this.Y = y;
        this.Tuiles = new Stack<Tuile>();
    }
    
}