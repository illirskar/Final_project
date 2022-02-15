using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClear : MonoBehaviour
{

    public UITimer Timer;
    public GameObject WinScreen;

    public Text WinTime;

    public AudioSource win;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Checks if the object in the field is tagged as a player. If true, activates the level clear screen.
        {
            win.Play();
            WinTime.text = "Your time is: " + Timer.TimerText.text;
            Timer.playing = false;
            WinScreen.SetActive(true);
        }
    }
}
