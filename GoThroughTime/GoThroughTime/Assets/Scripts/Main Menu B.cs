using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuB : MonoBehaviour
{
    // Load Scene
    public void Play()
    {
        SceneManager.LoadScene("tutorial");

    }

    //Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
