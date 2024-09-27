namespace Punto.objects;

public class Case
{
    public Tuile tuile { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public List<Tuile> tuiles { get; set; }
    
    public Case(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.tuiles = new List<Tuile>();
    }
    
}