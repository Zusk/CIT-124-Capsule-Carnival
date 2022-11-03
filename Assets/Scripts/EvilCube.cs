using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player tempPlayer = other.transform.GetComponent<Player>();
        if (other.CompareTag("Player"))
        {
            tempPlayer.isAlive = false;
        }
    }
}
