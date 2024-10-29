using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
   public Transform respawnPoint;
   void OnTriggerEnter(Collider other){
             if (other.gameObject.tag == "Respawn") {
                 gameObject.transform.position = respawnPoint.position;
             }
      }
}
