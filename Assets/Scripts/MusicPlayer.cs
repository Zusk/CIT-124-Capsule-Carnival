using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.transform.position;
    }
}
