using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource MainMusic;
    public AudioSource timePause;
    public AudioSource timeForward;
    public AudioSource timeBack;
    public AudioSource timePause_music;

    public bool moveForward = true;
	public bool moveBackward = false;

    public float MusicTimeStamp = 0.0f;

 
    void Start(){
        MainMusic.time = MusicTimeStamp;
        MainMusic.Play();
    }

    void Update(){
        // listeners to make this object respon to time controls:
		if (Input.GetButtonDown("platformPause")){
			if (!moveForward && !moveBackward){
				moveForward = true;
				moveBackward = false;

                MainMusic.time = MusicTimeStamp;
                MainMusic.Play();
                timePause_music.Stop();
            } else{
				moveForward = false;
				moveBackward = false;

                SetTimeStamp();
                timePause.Play();
                MainMusic.Stop();
                timePause_music.Play();
                //can dd a coroutine to manage when timepause_music starts
			}
		}
		if (Input.GetButtonDown("platformBack")){
			moveForward=false;
			moveBackward=true;

            timeBack.Play();
            MainMusic.time = MusicTimeStamp;
			MainMusic.Play();
            timePause_music.Stop();
		}
		if (Input.GetButtonDown("platformFor")){
			moveForward=true;
			moveBackward=false;

            timeForward.Play();
            MainMusic.time = MusicTimeStamp;
            MainMusic.Play();
            timePause_music.Stop();
		}
    }


    public void SetTimeStamp(){
        MusicTimeStamp = MainMusic.time;
    }

}
