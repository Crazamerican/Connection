using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimTrig : MonoBehaviour
{
    Animator anim;
    public int startTimer;
    public int normalTimer;
    int Timer;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        Timer = startTimer;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= Timer) {
            count = 0;
            anim.SetBool("ShootLaser", true);
            Timer = normalTimer;
        }
        if (anim.GetBool("EndOfShot")) {
            count++;
        }
    }
}
