using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProper : MonoBehaviour
{

    private GameObject currentActive;
    public GameObject returnPanel;
    bool paused = false;

    public GameObject[] panels;

    public GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && paused)
        {
            returnPanel.SetActive(false);
            paused = false;
            ReactivateCurrentPanel();
        }   
        if (Input.GetKeyUp(KeyCode.Escape) && !paused)
        {
            paused = true;
            returnPanel.SetActive(true);
            DeactivateCurrentPanel();
            returnPanel.GetComponent<ReturnPanelButtonScript>().SetFirstButton();
            returnPanel.GetComponent<ReturnPanelButtonScript>().Pause();

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DeactivateCurrentPanel();
        }
    }

    public void ResumeGame()
    {
        returnPanel.GetComponent<ReturnPanelButtonScript>().Resume();
        returnPanel.SetActive(false);
        
    }

    public void ActivatePanel(GameObject panelToAwaken)
    {
        Debug.Log("Attempting to turn on the panel" + panelToAwaken.name);
        currentActive = panelToAwaken;
        panelToAwaken.SetActive(true);
        panelToAwaken.GetComponent<PanelUiPlayerController>().SetWait(.05f);
    }

    public void DeactivateCurrentPanel()
    {
        if(currentActive != null)
        {
            currentActive.SetActive(false);
            currentActive.GetComponent<PanelUiPlayerController>().TurnPlayerUiOff();
        }
    }

    public void ReactivateCurrentPanel()
    {
        if (currentActive != null)
        {
            currentActive.SetActive(true);
            currentActive.GetComponent<PanelUiPlayerController>().TurnPlayerUiOn();
        }
    }
}
