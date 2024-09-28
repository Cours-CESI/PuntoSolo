namespace Punto.objects;

public class Tuile
{
    public Tuile(int number, string sPlayer)
    {
        this.Number = number;
        this.SPlayer = sPlayer;
    }
    
    public int Number { get; set; }
    public string SPlayer { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}