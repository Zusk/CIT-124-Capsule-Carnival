using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    // Start is called before the first frame update
    public float timeValue;
    // Update is called once per frame
    //This parses our time value into something that looks nicer.
    public string ParseTime()
    {
        int minutes = Mathf.FloorToInt(timeValue / 60F);
        int seconds = Mathf.FloorToInt(timeValue - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        return niceTime;
    }
    //This updates the time, then displays it in the reference text.
    void Update()
    {
        timeValue += Time.deltaTime;

        timer.text = ParseTime();
    }
}
