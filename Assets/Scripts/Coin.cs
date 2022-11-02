using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform model;
    public float speed = 500f;

    private void Start()
    {
        model = transform.GetChild(0);
    }
    void Update()
    {
        model.Rotate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Coin!");
            Destroy(this.gameObject);
        }
    }

}
