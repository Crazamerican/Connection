using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float width;
    float height;
    public GameObject rightAttack;
    public GameObject leftAttack;
    //true on start and when facing right, false when facing left
    bool rightFacing;
    //if you are attacking
    bool attacking;
    int attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        rightFacing = true;
        attacking = false;
        attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0)
        {
            rightFacing = true;
        } else if (moveHorizontal < 0)
        {
            rightFacing = false;
        }
        if (attacking == true)
        {
            attackTimer++;
            if (attackTimer == 20)
            {
                rightAttack.SetActive(false);
                leftAttack.SetActive(false);
                attackTimer = 0;
                attacking = false;
            }
        }
        if (Input.GetButtonDown("Attack") && attacking == false)
        {
            attacking = true;
            if (rightFacing == true)
            {
                rightAttack.SetActive(true);
            } else if (rightFacing == false)
            {
                leftAttack.SetActive(true);
            }
        }
    }
}
