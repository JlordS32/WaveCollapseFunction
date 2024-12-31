using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTileRule", menuName = "WFC/TileRule")]
public class TileRule : ScriptableObject
{
    public TileBase tile;
    public List<TileRule> upNeighbors;   
    public List<TileRule> downNeighbors; 
    public List<TileRule> leftNeighbors; 
    public List<TileRule> rightNeighbors;
}
