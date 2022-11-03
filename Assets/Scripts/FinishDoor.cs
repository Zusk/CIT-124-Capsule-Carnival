using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    //A number of necessary references for the finish function
    public Timer timerComp;
    public Player playerComp;
    public TextMeshProUGUI statsText;
    public GameObject infoPanel;
    public GameObject pointsDisplay;
    public GameObject finishDisplay;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //If the player touches the victory door, call the finish function!
        if (other.CompareTag("Player"))
        {
            Finish();
        }
    }
    void Finish()
    {
        //This function, called when the player enters the finish door, does a number of things!
        //First, it 'snapshots' the players score and time, to display at the very end.
        //It then goes through a similar process to the game over function found in player,
        //disabling movement, zeroing out velocity and hiding the player model.
        //It then prints the text to show - which is the combined score + time to the victory panel, then shows the
        //victory panel!
        string text_to_show = "Finish!\nCoins: " + playerComp.score + "/40\nTime: " + timerComp.ParseTime();
        playerComp.rigidbodyComponent.useGravity = false;
        playerComp.rigidbodyComponent.velocity = new Vector3(0, 0, 0);
        playerComp.playerModel.SetActive(false);
        playerComp.enabled = false;

        pointsDisplay.SetActive(false);
        infoPanel.SetActive(false);
        statsText.text = text_to_show;
        finishDisplay.SetActive(true);
    }
}
