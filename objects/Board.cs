namespace Punto.objects;

public class Board
{
    public List<List<Case>> Tray { get; set; }

    public Board()
    {
        this.Tray = new List<List<Case>>();
        
        for (int i = 0; i < 6; i++)
        {
            this.Tray.Add(new List<Case>());
            for (int j = 0; j < 6; j++)
            {
                this.Tray[i].Add(new Case(i, j));
            }
        }
    }

    public void SetTuile(int x, int y, Tuile tuile)
    {
        this.Tray[y][x].Tuiles.Push(tuile);
    }

    public void Write()
    {
        foreach (List<Case> col in this.Tray)
        {
            foreach (Case currentCase in col)
            {
                Console.Write("[" + currentCase.X + ";" + currentCase.Y + "] ");
            }
            Console.WriteLine();
        }
    }
}