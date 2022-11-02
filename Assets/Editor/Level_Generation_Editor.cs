using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Level_Generation))]
public class Level_Generation_Editor : Editor
{
    //This is an editor script to generate level data from pngs!
    public override void OnInspectorGUI()
    {
        //This script attaches itself to another, so just use the default inspector until we add our custom buttons
        DrawDefaultInspector();
        //Time to add our inspector buttons!
        Level_Generation Level_Generation_Reference_Script = (Level_Generation)target;
        //If you click "build level"
        //Gist ocf htis is that it runs the 
        if (GUILayout.Button("Build Level"))
        {
            Level_Generation_Reference_Script.Map_Logic();
            //GameObject envio = Level_Generation_Reference_Script.environmentHolder.gameObject;
            //SceneVisibilityManager.instance.Hide(envio, true);
        }
        //If you click "clear level"
        //Jist of this is that it clears the game object block objects.
        if (GUILayout.Button("Clear Level"))
        {
            Transform enviromentHolder = Level_Generation_Reference_Script.transform.GetChild(0);
            enviromentHolder.SetParent(null);
            int childNum = Level_Generation_Reference_Script.transform.childCount;
            for (int i = 0; i < childNum; i++)
            {
                //Have to use DestroyImmediate in editor scripts
                DestroyImmediate(Level_Generation_Reference_Script.transform.GetChild(0).gameObject);
            }
            enviromentHolder.SetParent(Level_Generation_Reference_Script.transform);
            childNum = enviromentHolder.childCount;
            for (int i = 0; i < childNum; i++)
            {
                DestroyImmediate(enviromentHolder.GetChild(0).gameObject);
            }
            //SceneVisibilityManager.instance.DisablePicking(enviromentHolder.gameObject, true);
        }
    }
}