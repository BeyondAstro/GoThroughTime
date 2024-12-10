using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn : MonoBehaviour
{
   public Transform respawnPoint;
   public string loadScene = "yssie";
   void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Player") {
                other.gameObject.transform.position = respawnPoint.position;
                SceneManager.LoadScene(loadScene);
            }
      }
}
