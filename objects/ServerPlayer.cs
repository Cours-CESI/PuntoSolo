namespace Punto.objects;

public class ServerPlayer
{
    public string Name { get; set; }
    public List<Tuile> TuilesMain { get; set; }
    public Stack<Tuile> Tuiles { get; set; }
    private readonly TileValidator _tileValidator; // Instance de la classe de validation

    public ServerPlayer(string name)
    {
        // Init Value
        Name = name;
        TuilesMain = new List<Tuile>();
        Tuiles = new Stack<Tuile>();
        _tileValidator = new TileValidator(); // Instanciation de la classe de validation

        // Set les piles des joueurs
        for (int i = 1; i <= 9; i++) 
        {
            for (int j = 0; j < 2; j++) 
            {
                Tuiles.Push(new Tuile(i, this.Name));
            }
        }
        Tuiles = Utils.ShuffleStack(Tuiles);
        SetMain();
    }

    public void SetMain()
    {
        for (int i = 0; i < 2; i++) 
        {
            TuilesMain.Add(Tuiles.Pop());
        }
    }

    public Tuile Turn(Game game)
    {
        // Vérifie s'il y a des tuiles à jouer
        if (TuilesMain.Count == 0)
            throw new InvalidOperationException("Aucune tuile à jouer.");

        Random r = new Random();
        Tuile usedTuile = TuilesMain[0];
        bool validMove = false;
        string errorMessage = string.Empty;

        // Calculer le décalage pour que le milieu soit (0,0)
        int offset = game.Board.GridSize / 2;

        while (!validMove)
        {
            // Logique de placement de la tuile
            if (game.isFirstTurn)
            {
                usedTuile.X = 0 + offset; // Centrer
                usedTuile.Y = 0 + offset;
                validMove = true;
                game.isFirstTurn = false;
            }
            else
            {
                // Coordonnées aléatoires
                usedTuile.X = r.Next(-offset, 2 * offset + 1);
                usedTuile.Y = r.Next(-offset, 2 * offset + 1);

                // Vérifie si le mouvement est valide
                (validMove, errorMessage) = _tileValidator.IsValidMove(game, usedTuile);

                if (!validMove)
                {
                    Console.WriteLine($"Mouvement invalide pour la tuile {usedTuile.Number} à ({usedTuile.X}, {usedTuile.Y}). Raison: {errorMessage}");
                }
            }
        }
        TuilesMain.RemoveAt(0);
        if(Tuiles.Count > 0) TuilesMain.Add(Tuiles.Pop());

        return usedTuile; // Retourne la tuile utilisée
    }
}
