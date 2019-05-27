using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private bool grounded;
    public float velocity;
    public float gravity;
    public GameObject otherPlayer;
    private bool onBox;

    // Use this for initialization
    void Start()
    {
        grounded = true;
        onBox = false;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //if jump button pressed and a character is on the ground
        if ((Input.GetButtonDown("Jump") && grounded))
        {
            Debug.Log("Jump");
            velocity = jumpTakeOffSpeed;
            grounded = false;
            onBox = false;
        }
        velocity = velocity - gravity;
        Collider2D[] top1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.5f, velocity + 0.5f), 0.01f);
        Collider2D[] top2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.5f, velocity + 0.5f), 0.01f);
        Collider2D[] bottom1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.5f, velocity - 0.5f), 0.01f);
        Collider2D[] bottom2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.5f, velocity - 0.5f), 0.01f);

        bool col = false;

        foreach (var collide in top1)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                if (collide.tag == "Ground")
                {
                    onBox = false;
                } else
                {
                    onBox = true;
                }
            }
        }
        foreach (var collide in top2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                if (collide.tag == "Ground")
                {
                    onBox = false;
                }
                else
                {
                    onBox = true;
                }
            }
        }
        foreach (var collide in bottom1)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                if (collide.tag == "Ground")
                {
                    onBox = false;
                }
                else
                {
                    onBox = true;
                }
            }
        }
        foreach (var collide in bottom2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                if (collide.tag == "Ground")
                {
                    onBox = false;
                }
                else
                {
                    onBox = true;
                }
            }
        }
        if ((otherPlayer.GetComponent<VerticalMovement>().grounded == true && otherPlayer.GetComponent<VerticalMovement>().onBox == true))
        {
            col = true;
        }
        if (col == false)
        {
            grounded = false;
        }
        if (col == true && velocity > 0)
        {
            velocity = 0;
        } else if (col == true && velocity < 0)
        {
            velocity = 0;
            grounded = true;
        }
        if (col == false)
        {
            transform.position = transform.position + new Vector3(0, velocity);
        }
    }

}
