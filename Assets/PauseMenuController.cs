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


    public void Start()
    {
        audioSource = go.GetComponent<AudioSource>();
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
    }

    public void RestartGame() {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    public void ReturnToMain() {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
