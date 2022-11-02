using UnityEditor;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    GameObject player;
    Vector3 Pos()
    {
        return player.transform.position;
    }
    [ExecuteInEditMode]
    private void Update()
    {
        if (!player)
        {
            player = GameObject.Find("bean(Clone)");
        }
        else
        {
            SceneView.lastActiveSceneView.camera.transform.position = new Vector3(Pos().x, Pos().y, Pos().z - 3);
            SceneView.lastActiveSceneView.LookAt(Pos());
        }
    }
}