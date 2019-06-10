using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMaster : MonoBehaviour
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private bool grounded;
    private bool grounded2;
    private float velocity;
    private float velocity2;
    public float gravity;
    public float gravity2;
    public GameObject otherPlayer;
    private bool onBox;
    private bool onBox2;
    //1 is top 0 is nothing -1 is bottom
    public int topOrBottom;
    public int topOrBottom2;

    // Use this for initialization
    void Start()
    {
        grounded = true;
        grounded2 = true;
        onBox = false;
        onBox2 = false;
        topOrBottom = 0;
        topOrBottom2 = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("button");
        }
        //if jump button pressed and a character is on the ground
        if (Input.GetButtonDown("Jump") && (grounded || grounded2))
        {
            velocity = jumpTakeOffSpeed;
            velocity2 = jumpTakeOffSpeed;
            grounded = false;
            grounded2 = false;
            onBox = false;
            onBox2 = false;
            Debug.Log("jump");
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        topOrBottom = 0;
        topOrBottom2 = 0;
        /*if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("button");
        }
        //if jump button pressed and a character is on the ground
        if (Input.GetButtonDown("Jump") && (grounded || grounded2))
        {
            velocity = jumpTakeOffSpeed;
            velocity2 = jumpTakeOffSpeed;
            grounded = false;
            grounded2 = false;
            onBox = false;
            onBox2 = false;
            Debug.Log("jump");
        }*/
        velocity = velocity - gravity;
        velocity2 = velocity2 - gravity2;
        Collider2D[] top1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.5f, velocity + 0.5f), 0.01f);
        Collider2D[] top2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.5f, velocity + 0.5f), 0.01f);
        Collider2D[] bottom1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.5f, velocity - 0.5f), 0.01f);
        Collider2D[] bottom2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.5f, velocity - 0.5f), 0.01f);
        Collider2D[] top1_2 = Physics2D.OverlapCircleAll(otherPlayer.transform.position + new Vector3(0.5f, velocity + 0.5f), 0.01f);
        Collider2D[] top2_2 = Physics2D.OverlapCircleAll(otherPlayer.transform.position + new Vector3(-0.5f, velocity + 0.5f), 0.01f);
        Collider2D[] bottom1_2 = Physics2D.OverlapCircleAll(otherPlayer.transform.position + new Vector3(0.5f, velocity - 0.5f), 0.01f);
        Collider2D[] bottom2_2 = Physics2D.OverlapCircleAll(otherPlayer.transform.position + new Vector3(-0.5f, velocity - 0.5f), 0.01f);

        bool col = false;
        bool col2 = false;

        foreach (var collide in top1)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                topOrBottom = 1;
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
        foreach (var collide in top2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                topOrBottom = 1;
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
                topOrBottom = -1;
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
                topOrBottom = -1;
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
        foreach (var collide in top1_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col2 = true;
                topOrBottom2 = 1;
                if (collide.tag == "Ground")
                {
                    onBox2 = false;
                }
                else
                {
                    onBox2 = true;
                }
            }
        }
        foreach (var collide in top2_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col2 = true;
                topOrBottom2 = 1;
                if (collide.tag == "Ground")
                {
                    onBox2 = false;
                }
                else
                {
                    onBox2 = true;
                }
            }
        }
        foreach (var collide in bottom1_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col2 = true;
                topOrBottom2 = -1;
                if (collide.tag == "Ground")
                {
                    onBox2 = false;
                }
                else
                {
                    onBox2 = true;
                }
            }
        }
        foreach (var collide in bottom2_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col2 = true;
                topOrBottom2 = -1;
                if (collide.tag == "Ground")
                {
                    onBox2 = false;
                }
                else
                {
                    onBox2 = true;
                }
            }
        }
        if (col == true)
        {
            col2 = true;
            topOrBottom2 = topOrBottom;
        }
        else if (col2 == true)
        {
            col = true;
            topOrBottom = topOrBottom2;
        } 
        if (col == false)
        {
            grounded = false;
        }
        if (col2 == false)
        {
            grounded2 = false;
        }
        if (col == true && topOrBottom == 1)
        {
            velocity = 0;
        }
        else if (col == true && topOrBottom == -1)
        {
            velocity = 0;
            grounded = true;
        }
        if (col2 == true && topOrBottom2 == 1)
        {
            velocity2 = 0;
        }
        else if (col2 == true && topOrBottom2 == -1)
        {
            velocity2 = 0;
            grounded2 = true;
        }
        if (col == false)
        {
            transform.position = transform.position + new Vector3(0, velocity);
        }
        if (col2 == false)
        {
            otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, velocity2);
        }
    }

}

