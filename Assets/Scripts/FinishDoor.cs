using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    public Timer timerComp;
    public Player playerComp;
    public TextMeshProUGUI statsText;
    public GameObject infoPanel;
    public GameObject pointsDisplay;
    public GameObject finishDisplay;
    public GameObject finishText;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Finish();
        }
    }
    void Finish()
    {
        string text_to_show = "Finish!\nCoins: " + playerComp.score + "/40\nTime: " + timerComp.ParseTime();
        playerComp.rigidbodyComponent.useGravity = false;
        playerComp.rigidbodyComponent.velocity = new Vector3(0, 0, 0);
        playerComp.playerModel.SetActive(false);
        playerComp.enabled = false;
        finishText.SetActive(true);

        pointsDisplay.SetActive(false);
        infoPanel.SetActive(false);
        statsText.text = text_to_show;
        finishDisplay.SetActive(true);
    }
}
