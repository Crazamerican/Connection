using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlScreen;
    public GameObject loadScreen;

    public void PlayGame() {
        SceneManager.LoadScene("NewGraphics1");
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

    public void LoadGame()
    {
        loadScreen.SetActive(true);
    }
}
