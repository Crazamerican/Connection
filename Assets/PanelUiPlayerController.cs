using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUiPlayerController : MonoBehaviour
{
    public GameObject playerUi1;
    public GameObject playerUi2;

    public void TurnPlayerUiOff()
    {
        playerUi1.GetComponent<MoveCharacterUI>().SetMoveable(false);
        playerUi2.GetComponent<MoveCharacterUI>().SetMoveable(false);
    }

    public void TurnPlayerUiOn()
    {
        playerUi1.GetComponent<MoveCharacterUI>().SetMoveable(true);
        playerUi2.GetComponent<MoveCharacterUI>().SetMoveable(true);
    }

    public void SetWait(float waitTime)
    {
        playerUi1.GetComponent<MoveCharacterUI>().SetWaitTime(waitTime);
        playerUi2.GetComponent<MoveCharacterUI>().SetWaitTime(waitTime);
    }

}
