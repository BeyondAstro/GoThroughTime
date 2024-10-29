using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
   public Transform respawnPoint;
   void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Player") {
                other.gameObject.transform.position = respawnPoint.position;
            }
      }
}
