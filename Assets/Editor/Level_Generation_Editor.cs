using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Level_Generation))]
public class Level_Generation_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Level_Generation myScript = (Level_Generation)target;
        if (GUILayout.Button("Build Level"))
        {
            myScript.newer_Map_Logic();
        }
        if (GUILayout.Button("Clear Level"))
        {
            int childNum = myScript.transform.childCount;
            for (int i = 0; i < childNum; i++)
            {
                DestroyImmediate(myScript.transform.GetChild(0).gameObject);
            }
        }
    }
}