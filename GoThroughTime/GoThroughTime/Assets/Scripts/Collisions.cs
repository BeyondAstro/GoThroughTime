using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour {
    public Progress_Bar progressBar;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            progressBar.CollectHand();
        }
    }

}
