using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class WaveCollapse
{
    // Variables
    private Cell[,] _cells;
    private TileRule _selectedCell;

    public WaveCollapse(int width, int height, List<TileRule> tileRules)
    {
        // Initialise 2D Cells of width and height
        _cells = new Cell[width, height];

        // Instantiate each cells
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // By default, the cell should not be collapsed and we give it all N tiles
                _cells[x, y] = new Cell(false, new List<TileRule>(tileRules));
            }
        }
    }

    // Callers should only calls this function to get the TileBase.
    // A for loop is required outside to construct the map.
    public TileBase GetTile(int x, int y)
    {
        // Start collapsing the cell from x, y.
        Collapse(x, y);
        return _cells[x, y].Options[0].tile;
    }

    private void Collapse(int x, int y)
    {
        // Get a random number
        int RandomIndex = Random.Range(0, _cells[x, y].Options.Count);

        // Update selected cell
        _selectedCell = _cells[x, y].Options[RandomIndex];

        // Collapse the cell and update tile
        _cells[x, y].IsCollapsed = true;
        _cells[x, y].Options = new List<TileRule> { _selectedCell };
        
        // Start propagating neighboring cells
        Propagate(x, y);
    }

    private void Propagate(int x, int y)
    {
        // Define directions
        Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        // Iterate through each direction
        foreach (Vector2Int direction in directions)
        {
            // Calculate offsets
            int newX = x + direction.x; 
            int newY = y + direction.y;

            // Check if the new coordinates are within bounds
            if (newX >= 0 && newY >= 0 && newX < _cells.GetLength(0) && newY < _cells.GetLength(1))
            {
                // Only propagate if the neighboring cell is not collapsed
                if (!_cells[newX, newY].IsCollapsed)
                {
                    _cells[newX, newY].UpdateCell(_selectedCell, direction);
                }
            }
        }
    }
}

public struct Cell
{
    public bool IsCollapsed;
    public List<TileRule> Options;
    public int Entropy;

    public Cell(bool isCollapsed, List<TileRule> options)
    {
        IsCollapsed = isCollapsed;
        Options = options;
        Entropy = options.Count;
    }

    public void UpdateCell(TileRule selectedCell, Vector2Int direction)
    {
        List<TileRule> validOptions = new List<TileRule>();

        foreach (TileRule rule in Options)
        {
            if (IsCompatible(selectedCell, rule, direction))
                validOptions.Add(rule);
        }

        Options = validOptions;
        Entropy = validOptions.Count;
    }

    public readonly bool IsCompatible(TileRule selectedCell, TileRule rule, Vector2Int direction)
    {
        return direction switch
        {
            var _ when direction == Vector2Int.up => selectedCell.upNeighbors.Contains(rule),
            var _ when direction == Vector2Int.down => selectedCell.downNeighbors.Contains(rule),
            var _ when direction == Vector2Int.left => selectedCell.leftNeighbors.Contains(rule),
            var _ when direction == Vector2Int.right => selectedCell.rightNeighbors.Contains(rule),
            _ => false,
        };
    }
}
