using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerGravity {
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

	// Use this for initialization
	void Start () {
		
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        //if jump button pressed and a character is on the ground
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        /*else if (Input.GetButtonUp("Jump"))
        {
            //makes it so holding jump button makes characters jump higher
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 1f;
            }
        }*/

        targetVelocity = move * maxSpeed;
    }
}
