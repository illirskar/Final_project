using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public void NextScene(string level) //Loads the next level based on name. Alternative option would be to create a scene stack and pick from it.
    {
        SceneManager.LoadScene(level);
    }

    public void ReloadScene() //Reloads the current level
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
