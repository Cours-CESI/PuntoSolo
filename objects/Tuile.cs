namespace Punto.objects;

public class Tuile
{
    public Tuile(int number, ServerPlayer sPlayer)
    {
        this.Number = number;
        this.SPlayer = sPlayer;
    }
    
    public int Number { get; set; }
    public ServerPlayer SPlayer { get; set; }
}