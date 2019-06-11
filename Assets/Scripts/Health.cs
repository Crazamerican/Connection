using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    int curHealth;
    bool dead;
    public float invulnTime; 
    float invuln;//invulnerability timer
    bool damageable;

    // Start is called before the first frame update
    void Start()
    {
        invuln = 0;
        curHealth = maxHealth;
        dead = false;
        damageable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if the invulnerability timeframe has ended, allow the target to me hittable again
        if(!damageable && invuln < Time.time)
        {
            damageable = true;
        }
    }

    public void takeDamage(int damage)
    {
        //target is possible to take damage
        if (damageable)
        {
            invuln = Time.time + invulnTime;
            curHealth -= damage;
            Debug.Log(gameObject.name + " has taken damage");
            Debug.Log("Current Health: " + curHealth);
            //if the target's health goes down to zero, kill the target. currently a placeholder
            if (curHealth <= 0)
            {
                Debug.Log(gameObject.name + " has died");
                dead = true;
            }
        }
    }

    //Not sure if this will be needed or not
    public bool getDead()
    {
        return dead;
    }

    //same as above
    public int getCurHealth()
    {
        return curHealth;
    }

}
