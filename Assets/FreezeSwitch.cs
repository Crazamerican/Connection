using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSwitch : MonoBehaviour
{
    GameManagementScript gameManagement;
    public bool switchLevel = false;
    bool startFreeze = false;
    public int timer;
    int counter = 0;
    bool usedSwitch = false;
    public int recharge;
    int counter2 = 0;
    VerticalMaster verticalMaster;

    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("EndOfLevel").GetComponent<GameManagementScript>();
        verticalMaster = GameObject.Find("Player").GetComponent<VerticalMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchLevel) {
            if (Input.GetButtonDown("Invert") && usedSwitch == false)
            {
                if (startFreeze != true) {
                    usedSwitch = true;
                    startFreeze = true;
                    gameManagement.freezePlayer = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (startFreeze) {
            if (counter < timer)
            {
                counter++;
            }
            else {
                counter = 0;
                startFreeze = false;
                gameManagement.freezePlayer = false;
            }
        }
        if (usedSwitch)
        {
            if (verticalMaster.forgiveGround == true || verticalMaster.forgiveGround2 == true)
            {
                counter2 = 0;
                usedSwitch = false;
            }
            else {
                if (counter2 < recharge)
                {
                    counter2++;
                }
                else
                {
                    counter2 = 0;
                    usedSwitch = false;
                }
            }
        }
    }
}
