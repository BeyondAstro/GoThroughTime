using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public void toCredits()
    {
        SceneManager.LoadScene("credits");
    }
    public void toMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
