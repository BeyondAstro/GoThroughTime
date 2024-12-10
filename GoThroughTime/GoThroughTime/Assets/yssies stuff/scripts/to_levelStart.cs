using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class to_levelStart : MonoBehaviour
{
    private bool playerNearby = false; 
    public GameObject direction;

    void Start(){
        direction.gameObject.SetActive(false);
    }
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.A)) // Detect player interaction
        {
            SwitchScenes();
        }

    }
    private void OnTriggerEnter(Collider book)
    {
        if (book.CompareTag("Player")) // Ensure it's the player interacting
        {
            Debug.Log("Book touched!");
            playerNearby = true;
            direction.gameObject.SetActive(true);
        }
    }

    public void SwitchScenes()
    {
        SceneManager.LoadScene("stretchedTower");
    }

}
