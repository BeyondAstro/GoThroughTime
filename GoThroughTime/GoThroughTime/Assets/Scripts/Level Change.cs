using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public string NextLevel = "End Scene";



    public void OnTriggerEnter(Collider other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(NextLevel);
        }

    }
}
