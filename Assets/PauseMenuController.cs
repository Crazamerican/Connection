using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public string level;
    public GameObject pauseMenu;
    public bool isPaused;
    public GameObject go;
    AudioSource audioSource;
    GameObject endOfLevel;


    public void Start()
    {
        audioSource = go.GetComponent<AudioSource>();
        endOfLevel = endOfLevel = GameObject.Find("EndOfLevel");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                audioSource.Play();
            }
            else {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                audioSource.Pause();
            }
        }
    }

    public void ResumeGame() {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        audioSource.Play();
    }

    public void RestartGame() {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMain() {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        endOfLevel.GetComponent<GameManagementScript>().SaveLevel();
        SceneManager.LoadScene("Main Menu");
    }
}
