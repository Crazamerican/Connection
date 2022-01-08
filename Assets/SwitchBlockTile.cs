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
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetButtonDown("Invert")) {
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
}
