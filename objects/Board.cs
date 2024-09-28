using System.Text;
using Microsoft.AspNetCore.SignalR;

namespace Punto.objects;

public class Board
{
    public readonly int GridSize = 11;
    public List<List<Case>> Tray { get; private set; } = new List<List<Case>>();
    
    public Board()
    {
        // Initialise un plateau de 11x11
        InitializeBoard(11, 11);
    }

    // Initialisation du plateau de taille `width x height`
    private void InitializeBoard(int width, int height)
    {
        for (int i = 0; i < height; i++)
        {
            var row = new List<Case>();
            for (int j = 0; j < width; j++) 
                row.Add(new Case(i, j));
            this.Tray.Add(row);
        }
    }

    public void Write()
    {
        StringBuilder sb = new StringBuilder();
        foreach (List<Case> row in this.Tray)
        {
            foreach (Case cell in row)
                sb.Append(cell.Tuiles.Count == 0 ? "[ N ] " : $"[{cell.Tuiles.Peek().Number} / {cell.Tuiles.Peek().SPlayer}] ");
            sb.AppendLine();
        }
        Console.Write(sb.ToString());
    }

    public async Task SendTray(IHubContext<ChatHub> hubContext, Game game)
    {
        try
        {
            /*foreach (List<Case> col in Tray)
            {
                foreach (Case currentCase in col)
                {
                    Console.WriteLine(currentCase.Tuiles.Count);
                    //if(currentCase.Tuiles.Count>0) Console.WriteLine(currentCase.Tuiles.Peek().SPlayer);
                }
            }*/

            await hubContext.Clients.All.SendAsync("ReceiveData", this.Tray.ToArray(), game.GameLoop, this.GridSize);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'envoi du plateau : {ex.Message}");
        }
    }
}