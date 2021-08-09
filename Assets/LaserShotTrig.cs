using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotTrig : MonoBehaviour
{
    Animator parent_Anim;
    Animator anim;
    DeathScript deathScript;
    // Start is called before the first frame update
    void Start()
    {
        parent_Anim = this.transform.parent.gameObject.GetComponent<Animator>();
        anim = this.GetComponent<Animator>();
        deathScript = FindObjectOfType<DeathScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent_Anim.GetBool("ShootLaser")) {
            anim.SetBool("BeginLaser", true);
        }
        if (parent_Anim.GetBool("CurLaser")) {
            anim.SetBool("EndLaser", true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && anim.GetBool("ActualLaser")) {
            deathScript.dead = true;
        }
    }
}
