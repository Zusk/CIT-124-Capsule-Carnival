using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilCube : MonoBehaviour
{
    //A veery simple script, just sets 'isAlive' to false in the player script when you
    //touch a evil cube.
    //The player script handles logic beyond this.
    private void OnTriggerEnter(Collider other)
    {
        Player tempPlayer = other.transform.GetComponent<Player>();
        if (other.CompareTag("Player"))
        {
            tempPlayer.isAlive = false;
        }
    }
}
