using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    // This tiny script just moves the boom box to the listener ( the main camera ).
    void LateUpdate()
    {
        transform.position = Camera.main.transform.position;
    }
}
