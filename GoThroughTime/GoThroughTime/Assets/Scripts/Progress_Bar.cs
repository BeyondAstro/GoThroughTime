using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Progress_Bar : MonoBehaviour {
    public GameObject clock; // The clock UI element
    public GameObject[] hands; // Array to hold hand UI elements (secondHand, minHand, hourHand)

    public GameObject secondHand; // The second hand element
    public GameObject minuteHand; // The minute hand element
    public GameObject hourHand; // The hour hand element
    public int gotHands = 0; // Counter for collected hands

    private bool finished = false;

    void Start() {
        
        gotHands = PlayerPrefs.GetInt("GotHands", 0); 
        
        for (int i = 0; i < hands.Length; i++){
            hands[i].SetActive(false);
        }
        
        clock.SetActive(true);
        secondHand.SetActive(true);
        minuteHand.SetActive(true);
        hourHand.SetActive(true);
        Debug.Log("Hands made");

    }

    void Update() {
        // Save progress periodically or when it changes
        PlayerPrefs.SetInt("GotHands", gotHands);
        if(gotHands== 2){
            finished = true;
            clock.SetActive(false);
            secondHand.SetActive(false);
            minuteHand.SetActive(false);
            hourHand.SetActive(false);
            SceneManager.LoadScene("You Win");  // Load the end scene when all hands are collected
        }
    }

    // Update the UI display based on collected hands
    public void UpdateStatsDisplay() {
        if (gotHands < hands.Length) {
            hands[gotHands].SetActive(true); // Activate the hand if it's within bounds
        } else {
            Debug.LogWarning("Attempted to activate a hand that doesn't exist in the array.");
        }
    }

    public void CollectHand() {
        if (gotHands < hands.Length) {
            showHand(gotHands); // Show the corresponding hand
            UpdateStatsDisplay(); // Update the UI
            gotHands++; // Increment after updating
        } else {
            Debug.LogWarning("Cannot collect more hands. All hands are already collected.");
        }
    }

    public void showHand(int gotHand) {
       if (gotHand == 0){
            secondHand.SetActive(false);
            Debug.Log("Hand collected");
        }
        if (gotHand == 1){
            minuteHand.SetActive(false);
            Debug.Log("Hand collected two");
        }
        if (gotHand == 2){
            hourHand.SetActive(false);
            Debug.Log("Hand collected three");
        }
    }

}