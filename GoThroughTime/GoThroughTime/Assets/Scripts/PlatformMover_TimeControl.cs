using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover_TimeControl : MonoBehaviour{

	public Transform[] movePoints;
	public float moveSpeed = 10f;
	float switchThreshold = 5f;

	int currentPoint = 0; //previous point
	int nextPoint = 1;
	public bool moveForward = true;
	public bool moveBackward = false;

	public bool makeLinear=false;

    void Start(){
		//starting position:
        transform.position = movePoints[currentPoint].position;
    }

	void Update(){
		// listeners to make this object respon to time controls:
		if (Input.GetButtonDown("platformPause")){
			moveForward=false;
			moveBackward=false;
		}
		if (Input.GetButtonDown("platformBack")){
			moveForward=false;
			moveBackward=true;
		}
		if (Input.GetButtonDown("platformFor")){
			moveForward=true;
			moveBackward=false;
		}
	}

    void FixedUpdate(){
		if (moveForward){
			//change currentPoint and nextPoint for forward movement:
			if (Vector3.Distance(transform.position, movePoints[nextPoint].position) < switchThreshold){
				//when we reach the end of the movePoints:
				if (nextPoint==movePoints.Length-1){
					//set a circle, set nextPoint back to 0:
					if (!makeLinear){
						nextPoint=0;
						currentPoint=movePoints.Length-1;
					} 
					//if linear, teleport to start:
					else{
						nextPoint=1;
						currentPoint=0;
						transform.position = movePoints[0].position;
					}
				} 
				//if a circle, set currentPoint back to 0:
				else if (currentPoint==movePoints.Length-1) {
					if (!makeLinear){
						nextPoint+=1;
						currentPoint=0;
					}
				}
				//set both points +1:
				else {
					nextPoint+=1;
					currentPoint+=1;
				}
				Debug.Log("NEW currentPoint (coming from) = " + currentPoint + ", NEW nextPoint (going to) = " + nextPoint);
			}
			//lerp forward:
			transform.position = Vector3.MoveTowards(transform.position, movePoints[nextPoint].position, moveSpeed * Time.fixedDeltaTime);
		}

		else if (moveBackward){
			//change currentPoint and nextPoint for backward movement:
			if (Vector3.Distance(transform.position, movePoints[currentPoint].position) < switchThreshold){
				//set currentPoint up to end:
				if (currentPoint==0){
					nextPoint -= 1;
					currentPoint=movePoints.Length-1;
				} 
				//set nextPoint up to end:
				else if (nextPoint==0){
					nextPoint =movePoints.Length-1;
					currentPoint-=1;
				} 
				//set both points -1:
				else {
					nextPoint-=1;
					currentPoint-=1;
				}
				Debug.Log("NEW nextPoint (coming from) = " + nextPoint + ", NEW currentPoint (going to) = " + currentPoint);
			}
			//lerp backward:
			transform.position = Vector3.MoveTowards(transform.position, movePoints[currentPoint].position, moveSpeed * Time.fixedDeltaTime);
		}
    }

	// allow player to ride moving platforms by making them children of platforms on contact:
	private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Player"){
            other.collider.transform.SetParent(transform); // so Player moves with platform
        }
    }

	// releasing player when player loses contact:
    private void OnCollisionExit(Collision other){
        if (other.gameObject.tag == "Player"){
            other.collider.transform.SetParent(null); // Player not parented when off platform
        }
    }

}
