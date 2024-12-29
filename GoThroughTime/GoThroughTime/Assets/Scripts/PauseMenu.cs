using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public static bool GameIsPaused = false;

    public GameObject controller;

    public GameObject pauseMenuUI;

    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;

    void Awake(){
        pauseMenuUI.SetActive(true); // so slider can be set
        SetLevel (volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    void Start(){
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        UnLockCursor();
    }

    void Update (){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        if (controller != null) {
            FPSController fpsController = controller.GetComponent<FPSController>();
            if (fpsController != null) {
                //fpsController.LockCursor();
                fpsController.mainMenu = false; // Set mainMenu to false
            }
        }
    }

    public void Pause (){
        if (!GameIsPaused){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        else{ Resume ();}
    }

    public void LoadMenu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame(){
        Debug.Log("Quitting game....");
        Application.Quit();
    }

    public void SetLevel(float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void UnLockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
