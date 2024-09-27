namespace Punto.objects;

public class Board
{
    public List<List<Case>> tray { get; set; }

    public Board()
    {
        this.tray = new List<List<Case>>();
        
        for (int i = 0; i < 6; i++)
        {
            this.tray.Add(new List<Case>());
            for (int j = 0; j < 6; j++)
            {
                this.tray[i].Add(new Case(i, j));
            }
        }
    }

    public void SetTuile(int x, int y, Tuile tuile)
    {
        this.tray[y][x].tuile = tuile;
    }
}