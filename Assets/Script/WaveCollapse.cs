using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Numerics;
using System.Collections;

public class WaveCollapse
{
    private Cell[,] _cells;
    private TileRule _selectedCell;

    public WaveCollapse(int width, int height, List<TileRule> tileRules)
    {
        _cells = new Cell[width, height];

        // Initialise each cells
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _cells[x, y] = new Cell(false, new List<TileRule>(tileRules));
            }
        }
    }

    public TileBase GetTile(int x, int y)
    {
        Collapse(x, y);
        return _cells[x, y].Options[0].tile;
    }

    private void Collapse(int x, int y)
    {
        // Get a random number
        int RandomIndex = Random.Range(0, _cells[x, y].Options.Count);

        // Update selected cell
        _selectedCell = _cells[x, y].Options[RandomIndex];

        // Collapse the cell
        _cells[x, y].IsCollapsed = true;
        _cells[x, y].Options = new List<TileRule> { _selectedCell };

        Propagate(x, y);
    }

    private void Propagate(int x, int y)
    {
        // Define the four possible directions (up, down, left, right)
        Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        // Iterate through each direction
        foreach (Vector2Int direction in directions)
        {
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
