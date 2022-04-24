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


    private SELECTOR_ENUM _ENUM;

    private GameObject endOfLevel;
    private GameManagementScript gameManager;
    private void Start()
    {
        endOfLevel = GameObject.Find("EndOfLevel");
        gameManager = endOfLevel.GetComponent<GameManagementScript>();
        SELECTOR_ENUM temp = gameManager.getEnum();
        if(!temp.Equals(SELECTOR_ENUM.NO_SELECT))
        {
            _ENUM = temp;
            selector.GetComponent<WorldSelectorScript>().SetUpArrowAndScene(_ENUM);
            SetENUM(SELECTOR_ENUM.NO_SELECT);
        }
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

    public enum SELECTOR_ENUM {
        TREE_WORLD,
        CAVE_WORLD,
        NO_SELECT
    }

    public SELECTOR_ENUM GetENUM()
    {
        return _ENUM;
    }

    public void SetENUM(SELECTOR_ENUM num)
    {
        _ENUM = num;
        gameManager.setEnum(num);
    }

}
