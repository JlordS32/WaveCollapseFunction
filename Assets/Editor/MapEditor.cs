using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TilePlacer))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Target script
        TilePlacer tilePlacer = (TilePlacer)target;

        // Draw default inspector fields
        DrawDefaultInspector();

        // If AutoUpdate is true, generate the map when the inspector is updated
        if (tilePlacer.AutoUpdate)
        {
            tilePlacer.GenerateMap();
        }

        if (GUILayout.Button("Generate"))
        {
            tilePlacer.GenerateMap();
        }
    }
}
