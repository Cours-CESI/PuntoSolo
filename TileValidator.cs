using Punto.objects;

namespace Punto;

public class TileValidator
{
    public (bool isValid, string reason) IsValidMove(Game game, Tuile tile)
    {
        int offset = game.Board.GridSize / 2;

        // Vérifie les limites
        if (tile.X + offset < 0 || tile.X + offset >= game.Board.GridSize || 
            tile.Y + offset < 0 || tile.Y + offset >= game.Board.GridSize)
        {
            return (false, "Hors des limites du plateau.");
        }

        // Vérifie les tuiles existantes
        Case targetCase = game.Board.Tray[tile.X + offset][tile.Y + offset];
        if (targetCase.Tuiles.Count > 0)
        {
            Tuile topTile = targetCase.Tuiles.Peek();
            if (tile.Number <= topTile.Number)
            {
                return (false, $"La tuile doit avoir un numéro supérieur à {topTile.Number}.");
            }
        }

        // Vérifie le nombre de tuiles dans la ligne et la colonne
        if (IsExceedingMaxLineLength(game.Board, tile, 6))
        {
            return (false, "Dépassement de la limite de 6 tuiles dans une ligne ou colonne.");
        }

        // Vérifie l'adjacence
        if (!HasAdjacentTiles(game.Board, tile))
        {
            return (false, "Aucune tuile adjacente n'est présente.");
        }

        return (true, string.Empty);
    }

    private bool HasAdjacentTiles(Board board, Tuile tile)
    {
        int offset = board.GridSize / 2;
        
        // Vérifie les cases adjacentes
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;

                int adjacentX = tile.X + dx + offset;
                int adjacentY = tile.Y + dy + offset;

                if (adjacentX >= 0 && adjacentX < board.GridSize && 
                    adjacentY >= 0 && adjacentY < board.GridSize &&
                    board.Tray[adjacentX][adjacentY].Tuiles.Count > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsExceedingMaxLineLength(Board board, Tuile tile, int maxLength)
    {
        int offset = board.GridSize / 2;
        int x = tile.X + offset;
        int y = tile.Y + offset;

        // Compte le nombre de tuiles dans la ligne
        int horizontalCount = CountTilesInLine(board, x, y, true);
        int verticalCount = CountTilesInLine(board, x, y, false);

        return horizontalCount > maxLength || verticalCount > maxLength;
    }

    private int CountTilesInLine(Board board, int x, int y, bool isHorizontal)
    {
        int count = 1; // Compte la tuile actuelle

        // Vérification dans la direction appropriée
        if (isHorizontal)
        {
            for (int i = x - 1; i >= 0; i--) // Gauche
            {
                if (board.Tray[i][y].Tuiles.Count > 0)
                    count++;
                else
                    break;
            }
            for (int i = x + 1; i < board.GridSize; i++) // Droite
            {
                if (board.Tray[i][y].Tuiles.Count > 0)
                    count++;
                else
                    break;
            }
        }
        else
        {
            for (int i = y - 1; i >= 0; i--) // Haut
            {
                if (board.Tray[x][i].Tuiles.Count > 0)
                    count++;
                else
                    break;
            }
            for (int i = y + 1; i < board.GridSize; i++) // Bas
            {
                if (board.Tray[x][i].Tuiles.Count > 0)
                    count++;
                else
                    break;
            }
        }

        return count;
    }
}
