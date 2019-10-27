using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlScreen;

    public void PlayGame() {
        SceneManager.LoadScene("Level 0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlMenu()
    {
        //SceneManager.LoadScene("Control Screen");
        controlScreen.SetActive(true);
    }

    public void BackMainMenu()
    {
        controlScreen.SetActive(false);
    }
}
