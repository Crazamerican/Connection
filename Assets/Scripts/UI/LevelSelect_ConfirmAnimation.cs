using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect_ConfirmAnimation : MonoBehaviour
{
    public string nameAnim = "ChibiDude";
    bool playing = false;
    public GameObject transition;
    public bool player1 = false;
    // Update is called once per frame
    void Update()
    {
        Animator thisAnim = GetComponent<Animator>();
        if (Input.GetButtonDown("Jump")) {
            thisAnim.Play(nameAnim + "_Confirm");
        }
        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName(nameAnim + "_Confirm")) {
            playing = true;
        }
        if (!thisAnim.GetCurrentAnimatorStateInfo(0).IsName(nameAnim + "_Confirm") && playing && player1)
        {
            Debug.Log("hey!");
            Animator anim = transition.GetComponent<Animator>();
            anim.Play("TransitionUI");
            playing = false;
        }
    }
}
