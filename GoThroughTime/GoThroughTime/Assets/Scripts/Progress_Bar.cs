using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Progress_Bar : MonoBehaviour {
    private GameObject player;
    public GameObject clock; // The clock UI element
    public GameObject[] hands; // Array to hold hand UI elements (secondHand, minHand, hourHand)

    public GameObject secondHand; // The second hand element
    public GameObject minuteHand; // The minute hand element
    public GameObject hourHand; // The hour hand element
    public static int gotHands = 0; // Counter for collected hands

    private bool finished = false;

    void Start() {
        player = GameObject.FindWithTag("Player");
        UpdateStatsDisplay();
        InitializeHands();
    }

    // Initialize hands to be inactive
    private void InitializeHands() {
        foreach (GameObject hand in hands) {
            hand.SetActive(false); // Deactivate all hands initially
        }
        clock.SetActive(true); // Ensure the clock is active
    }

    // Update the UI display based on collected hands
    public void UpdateStatsDisplay() {
        // Activate the clock and the hands based on the number of collected hands
        for (int i = 0; i < gotHands; i++) {
            if (i < hands.Length) {
                hands[i].SetActive(true); // Activate the hand corresponding to the collected count
            }
        }
    }

    // Call this method when the player collects a hand
    public void CollectHand() {
        gotHands++;
        UpdateStatsDisplay();
        if(gotHands == 2){
            SceneManager.LoadScene("LastLevel");
        }
        if (gotHands == 3){
            finished = true;
            //SceneManager.LoadScene("FinalScene");

        }
        
    }

    // Example method to detect collision with collectible hands
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hand")) {
            CollectHand();
            Destroy(other.gameObject); // Destroy the collected hand object
        }
    }

}