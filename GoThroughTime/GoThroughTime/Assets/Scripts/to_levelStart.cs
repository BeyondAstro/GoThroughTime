using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class to_levelStart : MonoBehaviour
{
    private bool playerNearby = false; 
    private bool bookTouched = false; 
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
        if (playerNearby && bookTouched){
            direction.gameObject.SetActive(true);
        }
        if(!playerNearby && bookTouched){
            direction.gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider book)
    {
        if (book.CompareTag("Player")) // Ensure it's the player interacting
        {
            Debug.Log("Book touched!");
            bookTouched = true;
            playerNearby = true;
        }
    }

    public void SwitchScenes()
    {
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().SetTimeStamp();
        SceneManager.LoadScene("stretchedTower");
    }

}
