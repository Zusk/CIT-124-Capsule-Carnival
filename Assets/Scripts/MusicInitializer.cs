using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInitializer : MonoBehaviour
{
    private static bool loadedPrefab = false;
    public GameObject boomBox;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (loadedPrefab == false)
        {
            GameObject tempBoomBox = Instantiate(boomBox, transform.position, Quaternion.identity);
            tempBoomBox.transform.parent = transform;
            loadedPrefab = true;
        }
    }
}
