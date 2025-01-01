using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[System.Serializable]
public class PerlinMapGenerator
{
    // Serialized Fields
    [SerializeField] private float _noiseScale;
    [SerializeField] private TerrainType[] _terrainType;
    
    // Variables
    private TileBase[,] _tiles;

    public TileBase[,] Generate(int width, int height)
    {
        // Generate the Perlin noise map
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, _noiseScale);

        // Initialize the TileBase array
        _tiles = new TileBase[width, height];

        // Map the noise values to terrain types
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float currentHeight = noiseMap[x, y];

                // Determine the appropriate terrain tile based on noise height
                foreach (TerrainType terrain in _terrainType)
                {
                    if (currentHeight <= terrain.NoiseHeight)
                    {
                        _tiles[x, y] = terrain.tile;
                        break;
                    }
                }
            }
        }

        return _tiles;
    }

}

[System.Serializable]
public struct TerrainType {
    public string Name;
    public float NoiseHeight;
    public TileBase tile;
}