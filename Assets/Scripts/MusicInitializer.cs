using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles music persistence on reload. Its jarring if the music in the scene
//resets on level load, lets use this script to make the music player persist!
public class MusicInitializer : MonoBehaviour
{
    //Use this bool to track the music player, the 'boom box'
    //Because this is a static bool, there is only a single instance of it between script
    //instances, this means that other music initializers wont be able to create their boom
    //boxes after this one has.
    private static bool loadedPrefab = false;
    //The boom box prefab to spawn!
    public GameObject boomBox;

    void Awake()
    {
        //This object stays around
        DontDestroyOnLoad(this.gameObject);
        //If we haven't spawned a boom box yet
        if (loadedPrefab == false)
        {
            //Spawn a boom box, set its parent, then set the loadedprefab bool to true.
            GameObject tempBoomBox = Instantiate(boomBox, transform.position, Quaternion.identity);
            tempBoomBox.transform.parent = transform;
            loadedPrefab = true;
        }
    }
}
