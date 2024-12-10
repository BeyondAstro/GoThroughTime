using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start_platformer : MonoBehaviour
{
    public GameObject panel; // Reference to the panel you want to hide
    public Button closeButton; // Reference to the button that will trigger the action

   void Update(){
     // Check if the player presses the space bar
     if (Input.GetKeyDown(KeyCode.A))
        {
           HideAllUI();
        }
   }

    public void HideAllUI()
    {
        panel.SetActive(false); // Disable the panel
        closeButton.gameObject.SetActive(false); // Disable the button
    }

}
