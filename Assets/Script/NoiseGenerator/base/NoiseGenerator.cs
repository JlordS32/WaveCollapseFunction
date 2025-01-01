using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class NoiseGenerator : ScriptableObject {
    [Header ("Base Noise Generator Params")]
    [SerializeField] protected float _noiseScale;
    [SerializeField] protected int _seed;
    [SerializeField] protected int _octaves;
    [SerializeField] protected float _lacunarity;
    [Range(0, 1)]
    [SerializeField] protected float _persistance;
    [SerializeField] protected Vector2 _offset;

    // Variables
    protected Tile[,] _tiles;

    // Abstract methods
    public abstract Tile[,] Generate(int width, int height);
}