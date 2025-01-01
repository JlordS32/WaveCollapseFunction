using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
[CreateAssetMenu(fileName = "New PerlinMapGenerator", menuName = "NoiseGenerators/Precipitation")]
public class PrecipitationMapGenerator : NoiseGenerator
{
    public override Tile[,] Generate(int width, int height) {
        return new Tile[width, height];
    }
}