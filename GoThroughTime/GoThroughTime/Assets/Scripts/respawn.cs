using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn : MonoBehaviour
{
   public Transform respawnPoint;
   public GameObject blocked;

   void Start(){
      blocked.gameObject.SetActive(false);
   }
   void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Player") {
                other.gameObject.transform.position = respawnPoint.position;
                blocked.gameObject.SetActive(true);
            }
      }
}
