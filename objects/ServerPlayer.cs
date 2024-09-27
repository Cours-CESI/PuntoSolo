using System.Reflection.Metadata;

namespace Punto.objects;

public class ServerPlayer
{
    public String Name { get; set; }
    public List<Tuile> TuilesMain { get; set; }
    public Stack<Tuile> Tuiles { get; set; }
    public ServerPlayer(string name)
    {
        // Init Value
        this.Name = name;
        TuilesMain = new List<Tuile>();
        Tuiles = new Stack<Tuile>();

        // Set les piles des joueurs
        for (int i = 1; i <= 9; i++) for (int j = 0; j < 2; j++) this.Tuiles.Push(new Tuile(i, this));
        this.Tuiles = Utils.ShuffleStack(Tuiles);
        // Set la main courant du joueur 
        this.SetMain();
    }
    public void SetMain()
    {
        for (int i = 0; i < 2; i++) this.TuilesMain.Add(this.Tuiles.Pop());
    }

    public Tuile Turn(Board board)
    {
        Random r = new Random();
        
        // Choix Tuile
        Tuile usedTuile = this.TuilesMain[0];
        usedTuile.X = r.Next(0, 6);
        usedTuile.Y = r.Next(0, 6);
        // Logique Regive Tuile
        this.TuilesMain.RemoveAt(0);
        this.TuilesMain.Add(this.Tuiles.Pop());
        
        return usedTuile;
    }
}