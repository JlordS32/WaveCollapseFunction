using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New PerlinMapGenerator", menuName = "NoiseGenerators/Precipitation")]
[System.Serializable]
public class PrecipitationMapGenerator : NoiseGenerator
{
    public override Tile[,] Generate(int width, int height) {
        return new Tile[width, height];
    }
}