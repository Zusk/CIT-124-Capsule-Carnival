using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//This script handles coin logic! Doing things such as updating the players score, creating
//a particle on pickup, playing the sound, then hiding the coin when that is all done.
public class Coin : MonoBehaviour
{
    //The model to hide
    private Transform model;
    //The speed that the coin rotates
    public float rotationSpeed = 500f;
    //The particle to spawn
    public GameObject collectParticle;
    //The audio component to play the collect noise from
    private AudioSource audioComponent;

    private void Start()
    {
        //Sets references
        model = transform.GetChild(0);
        audioComponent = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        //Rotate the coin!
        model.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //The player has collected the coin!
        if (other.CompareTag("Player"))
        {
            //Get the reference
            Player tempPlayer = other.transform.GetComponent<Player>();
            //Play the collect noise
            audioComponent.Play();
            //Add to the score
            tempPlayer.score++;
            //Calls the "collect coin function", which handles further logic
            CollectCoin(other.transform.GetComponent<Player>().ui_text, tempPlayer.score);
        }
    }
    private void CollectCoin(TextMeshProUGUI uiText, int value)
    {
        //Prints the coin text
        uiText.text = "Coins " + value.ToString() + "/40";
        //Create the particle
        GameObject coinDust = Instantiate(collectParticle, transform.position, Quaternion.identity);
        //Destroys the particle after some time
        Destroy(coinDust, 5f);
        //Disables the coin's visual model
        model.gameObject.SetActive(false);
        //Disables the coins hitbox, so that it cant be picked up again. This is done
        //instead of destroying the coin as the coin needs to persist in order to
        //play its noise.
        this.transform.GetComponent<BoxCollider>().enabled = false;
    }

}
