using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathFind))]
public class ExampleScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Keeps the default inspector fields

        PathFind script = (PathFind)target;
        if (GUILayout.Button("Run MyFunction"))
        {
            script.Path();
        }
    }
}