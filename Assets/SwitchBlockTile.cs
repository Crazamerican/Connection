using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlockTile : MonoBehaviour
{
    public Sprite realBlock;
    public Sprite transparentBlock;
    private SpriteRenderer spriteRend;
    BoxCollider2D col;
    private Animator anim;
    int timer;
    int counter = 0;
    bool startSwitch = false;
    bool usedSwitch = false;
    int recharge;
    int counter2 = 0;
    VerticalMaster verticalMaster;
    FreezeSwitch freezeSwitch;
    bool oldGround;
    bool oldGround2;
    // Start is called before the first frame update
    void Start()
    {
        freezeSwitch = GameObject.Find("Characters_V4").GetComponent<FreezeSwitch>();
        verticalMaster = GameObject.Find("Player").GetComponent<VerticalMaster>();
        recharge = freezeSwitch.recharge;
        timer = freezeSwitch.timer;
        anim = this.GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        if (col.enabled)
        {
            spriteRend.sprite = realBlock;
        }
        else
        {
            spriteRend.sprite = transparentBlock;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Invert") && startSwitch == false && usedSwitch == false) {
            usedSwitch = true;
            startSwitch = true;
            col.enabled = !col.enabled;
            if (col.enabled)
            {
                anim.SetBool("FadeIn", true);
                anim.SetBool("StartAnim", true);
            }
            else {
                anim.SetBool("FadeIn", false);
                anim.SetBool("StartAnim", true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (startSwitch)
        {
            if (counter < timer)
            {
                counter++;
            }
            else
            {
                counter = 0;
                startSwitch = false;
            }
        }
        if (usedSwitch)
        {
            if ((verticalMaster.forgiveGround == true || verticalMaster.forgiveGround2 == true) && (oldGround == false || oldGround2 == false))
            {
                counter2 = 0;
                usedSwitch = false;
            }
            else
            {
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
        oldGround = verticalMaster.forgiveGround;
        oldGround2 = verticalMaster.forgiveGround2;
    }
}
