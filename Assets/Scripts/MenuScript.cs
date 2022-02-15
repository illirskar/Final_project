using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject levelSelect;
    public GameObject menu;

    public void TerminateApp() //Quits the game.
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        levelSelect.SetActive(true);
        menu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        menu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void PickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
