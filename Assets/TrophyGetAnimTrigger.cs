using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyGetAnimTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Animator anim = this.GetComponent<Animator>();
            anim.SetBool("TrophyGet", true);
        }
    }
}
