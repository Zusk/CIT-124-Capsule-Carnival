using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform model;
    public float speed = 500f;
    public GameObject collectParticle;
    private AudioSource audioComponent;

    private void Start()
    {
        model = transform.GetChild(0);
        audioComponent = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        model.Rotate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Player tempPlayer = other.transform.GetComponent<Player>();
        audioComponent.Play();
        tempPlayer.score++;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin!");
            CollectCoin(other.transform.GetComponent<Player>().ui_text, tempPlayer.score);
        }
    }
    private void CollectCoin(TextMeshProUGUI uiText, int value)
    {
        uiText.text = "Coins " + value.ToString() + "/40";
        GameObject coinDust = Instantiate(collectParticle, transform.position, Quaternion.identity);
        Destroy(coinDust, 5f);
        model.gameObject.SetActive(false);
        this.transform.GetComponent<BoxCollider>().enabled = false;
    }

}
