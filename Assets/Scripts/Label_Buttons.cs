using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script handles button logic for the reset and quit buttons, both in the victory panel
//and the info panel
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
