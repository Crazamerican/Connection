using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivate : MonoBehaviour
{
    bool nearPlayer;
    SwitchMaster switchMaster;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        nearPlayer = false;
        switchMaster = GameObject.Find("SwitchMaster").GetComponent<SwitchMaster>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Invert") && nearPlayer) {
            switchMaster.Switch1 = !switchMaster.Switch1;
            anim.SetBool("Switch1", switchMaster.Switch1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            nearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            nearPlayer = false;
        }
    }
}
