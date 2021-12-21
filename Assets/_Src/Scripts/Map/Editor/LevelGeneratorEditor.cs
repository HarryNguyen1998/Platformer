using UnityEngine;
using UnityEditor;

namespace MyPlatformer
{
    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelGenerator levelGen = (LevelGenerator)target;

            if (GUILayout.Button("Generate"))
            {
                levelGen.GenerateMap();
            }

            if (GUILayout.Button("Clear"))
            {
                levelGen.ClearMap();
            }
        }
    }

}
