using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnPanelButtonScript : MonoBehaviour
{
    public GameObject panel;
    public GameObject resume;
    public GameObject player1UI;
    public GameObject player2UI;
    public GameObject selector;

    public void SetFirstButton()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(resume);
    }

    public void Resume()
    {
        player1UI.GetComponent<MoveCharacterUI>().SetMoveable(true);
        player2UI.GetComponent<MoveCharacterUI>().SetMoveable(true);
        selector.GetComponent<WorldSelectorScript>().SetMoveable(true);
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        player1UI.GetComponent<MoveCharacterUI>().SetMoveable(false);
        player2UI.GetComponent<MoveCharacterUI>().SetMoveable(false);
        selector.GetComponent<WorldSelectorScript>().SetMoveable(false);
        Time.timeScale = 0f;
    }

    public void ToWorldSelect()
    {
        //Still need to implement both the UI and how this will work. 
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("TitleCard_Demo");
    }
}
