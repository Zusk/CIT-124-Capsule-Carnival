using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Label_Buttons : MonoBehaviour
{
    //Called from canvas button
    public void ButtonReset()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    //Called from canvas button
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
