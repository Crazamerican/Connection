using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    float timeFrame;
    public bool facingRight;
    float offset;
    Vector3 startPos;
    bool attackDone;
    bool startAttack;
    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y);
        timeFrame = 0;
        offset = facingRight ? 2f: -2f; //where the enemy will be looking for the player
        attackDone = false;
        startAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(offset, 0);
        Debug.DrawRay(transform.position, target); //I like being able to see the ray while viewing the scene
        //if the attack timer has ended, try and attack
        if (timeFrame < Time.time)
        {
            attemptAttack(target);
        }
        //if attemptAttack found the player, attack
        if(startAttack == true)
        {
            attack(target);
        }
    }
    
    //checks in front of the enemy for the player and if it finds the player, tells the enemy to attack 
    void attemptAttack(Vector3 target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target);
        if(hit.collider != null && (hit.collider.gameObject.name.Equals("Playuer1") || hit.collider.gameObject.name.Equals("Player2"))) // && hit.collider.gameObject.CompareTag("Player"))
        {
            startAttack = true;
        }
    }

    //Kinda just runs in front a little ways and then runs back, nothing special
    void attack(Vector3 target)
    {
        //forward movement with the "attack"
        if(!attackDone)
        {
            transform.position = new Vector3(transform.position.x + (.1f * offset), transform.position.y);
            if(Mathf.Abs(transform.position.x - (startPos.x + target.x)) < .2f)
            {
                transform.position = new Vector3(startPos.x + target.x, transform.position.y);
                attackDone = true;
            }
        }
        if(Mathf.Abs(transform.position.x - startPos.x) < .1) //checking if the enemy has moved back to the start position
        {
            transform.position = startPos;
            timeFrame = Time.time + 2f;
            startAttack = false;
            attackDone = false;

        }
        else //moving back to the start position
        {
            transform.position = new Vector3(transform.position.x - (.05f * offset), transform.position.y);
        }

    }
    //If the enemy makes contact with the player, deal damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<Health>().takeDamage(1);
        }
    }
}
