﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMaster : MonoBehaviour
{
    //speed players initial jump is set to affects jump height as well
    public float jumpTakeOffSpeed = 7;

    [SerializeField] private bool grounded;
    [SerializeField] private bool grounded2;
    //player current speed
    float velocity;
    float velocity2;
    public float gravity;
    public float gravity2;
    //otherPlayer is Player2
    public GameObject otherPlayer;
    //1 is top 0 is nothing -1 is bottom
    private int topOrBottom;
    private int topOrBottom2;
    float distToCol;
    float distToCol2;
    //used for gravity inversion code
    private bool inverted;
    private bool inverted2;
    private bool inverted2_2;
    private bool invertOnCommand;
    //script to grab parameters from Player2
    VerticalOther otherScript;
    //the width and height of Player1
    float width;
    float height;
    //float width2;
    //float height2;

    Animator charAnim;
    Animator otherCharAnim;

    public AudioClip jumpSound;
    AudioSource audioSource;

    bool moving;
    bool isSet;

    bool moving2;
    bool isSet2;

    GameObject box;
    public GameObject char_base;
    public GameObject ghost;

    public GameObject cameraBoi;
    FollowPlayer cameraScript;
    //similar to grounded but with a bit of extra wiggle room so player can be near ground and still jump
    bool forgiveGround;
    bool forgiveGround2;

    bool floatTop;
    int floatTimer;

    public bool inTunnel;

    public bool onMoving;
    public float onSpeed;
    public bool onMoving2;
    public float onSpeed2;

    private bool hiddenGroundFlag1;
    private bool hiddenGroundFlag2;

    // Use this for initialization
    void Start()
    {
        onMoving = false;
        onSpeed = 0f;
        onMoving2 = false;
        onSpeed2 = 0f;
        inTunnel = false;
        floatTimer = 0;
        floatTop = false;
        cameraScript = cameraBoi.GetComponent<FollowPlayer>();
        Application.targetFrameRate = 60;
        grounded = true;
        grounded2 = true;
        forgiveGround = true;
        forgiveGround2 = true;
        topOrBottom = 0;
        topOrBottom2 = 0;
        inverted = false;
        otherScript = otherPlayer.GetComponent<VerticalOther>();
        inverted2_2 = false;
        invertOnCommand = false;
        width = GetComponent<BoxCollider2D>().bounds.size.x;
        height = GetComponent<BoxCollider2D>().bounds.size.y;
        //width2 = otherScript.width;
        //height2 = otherScript.height;
        charAnim = GetComponentInChildren<Animator>();
        otherCharAnim = otherPlayer.GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        moving = false;
        isSet = false;
        moving2 = false;
        isSet2 = false;
        box = null;
        hiddenGroundFlag1 = false;
        hiddenGroundFlag2 = false;

    }

    private void Update()
    {
        //if jump button pressed and a character is on or extremely near the ground and not frozen
        if (Input.GetButtonDown("Jump") && (forgiveGround || forgiveGround2) && cameraScript.freezePlayers == false)
        {
            //jumps in opposite direction if inverted
            if (inverted == true || inverted2 == true)
            {
                velocity = -jumpTakeOffSpeed;
                velocity2 = -jumpTakeOffSpeed;
            }
            else
            {
                velocity = jumpTakeOffSpeed;
                velocity2 = jumpTakeOffSpeed;
            }
            grounded = false;
            grounded2 = false;
            forgiveGround = false;
            forgiveGround2 = false;
            audioSource.PlayOneShot(jumpSound, 0.7F);

        }
        //if gotten gravity inverting powerup, user can invert gravity while grounded
        if (Input.GetButtonDown("Invert") && invertOnCommand == true && (grounded || grounded2) && cameraScript.freezePlayers == false)
        {
            inverted2 = !inverted2;
            inverted = !inverted;
            gravity2 = gravity2 * -1;
            gravity = gravity * -1;
        }
        if (grounded || grounded2)
        {
            //charAnim.SetTrigger("grounded");
            //otherCharAnim.SetTrigger("grounded");
        }


    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        onMoving = false;
        onSpeed = 0f;
        onMoving2 = false;
        onSpeed2 = 0f;
        moving = false;

        if (inverted2 != otherScript.inverted2 && inverted2_2 == false)
        {
            invertOnCommand = true;
        }
        topOrBottom = 0;
        topOrBottom2 = 0;
        //once at height of jump, hovers for 5 frames
        if (velocity > 0 && velocity - gravity < 0 || floatTop == true)
        {
            velocity = 0;
            velocity2 = 0;
            floatTop = true;
            if (floatTimer > 5)
            {
                floatTop = false;
                floatTimer = 0;
            }
            floatTimer++;
        } //if in upward windtunnel sets velocity to .15f
        else if (inTunnel == true)
        {
            velocity = 0.15f;
            velocity2 = 0.15f;
        } //incorperates velocity to gravity
        else
        {
            velocity = velocity - gravity;
            velocity2 = velocity2 - gravity2;
        }
        //sets a bunch of colliders based on where the player will be if you incorperate the velocity
        Collider2D[] topboi = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity + height / 2), transform.position + new Vector3(+width / 2, velocity + height / 2 + .01f));
        Collider2D[] bottomboi = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity - height / 2), transform.position + new Vector3(+width / 2, velocity - height / 2 - .01f));
        Collider2D[] bottomboi_Moving = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity - height / 2), transform.position + new Vector3(+width / 2, velocity - height / 2 - .01f));
        //forgiving collider goes a bit further than regular colliders
        Collider2D[] bottom1_forgive = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity - height / 2), transform.position + new Vector3(+width / 2, velocity - height / 2 - .5f));
        //player2 colliders from VerticalOther script
        Collider2D[] topboi_2 = otherScript.GetTopBoi(velocity2);
        Collider2D[] bottomboi_2 = otherScript.GetBotBoi(velocity2);
        Collider2D[] bottomboi_2_Moving = otherScript.GetBotBoi_Moving(velocity2);
        Collider2D[] bottom1_2_Forgive = otherScript.GetBot_Forgive(velocity2);

        //charAnim.SetFloat("verticalSpeed", velocity);
        //otherCharAnim.SetFloat("verticalSpeed", velocity);

        //see if Player1 or Player2 collides with anything
        bool col = false;
        bool col2 = false;

        bool colForgive = false;

        distToCol = Mathf.Infinity;
        distToCol2 = Mathf.Infinity;

        //goes through collider topboi to see if Player 1 collided with anything on top of it
        foreach (var collide in topboi)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                topOrBottom = 1;
                distToCol = GetComponent<BoxCollider2D>().Distance(collide).distance;
            }
        }
        //see if player1 is colliding with something on it's bottom
        foreach (var collide in bottomboi)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                Debug.Log("bottom player2");
                col = true;
                topOrBottom = -1;
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving = true;
                    box = collide.gameObject;
                }
            }
        }

        foreach (var collide in bottomboi_Moving)
        {
            if (collide.gameObject.tag == "MoveBox")
            {
                onMoving = true;
                onSpeed = collide.gameObject.GetComponent<HorizontalBox>().speed;
            }
        }

        //same collider checks with player2
        foreach (var collide in topboi_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                distToCol2 = otherPlayer.GetComponent<BoxCollider2D>().Distance(collide).distance;
                col2 = true;
                topOrBottom2 = 1;
            }
        }

        foreach (var collide in bottomboi_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                Debug.Log("bottom player2");
                col2 = true;
                topOrBottom2 = -1;
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving2 = true;
                    box = collide.gameObject;
                }
            }
        }

        foreach (var collide in bottomboi_2_Moving)
        {
            if (collide.gameObject.tag == "MoveBox")
            {
                Debug.Log("onMovingBox");
                onMoving2 = true;
                onSpeed2 = collide.gameObject.GetComponent<HorizontalBox>().speed;
            }
        }

        //if either player is on ground, then stops player from moving vertically.
        if (grounded || grounded2)
        {
            velocity = 0;
            velocity2 = 0;
        }
        //same collider but with the forgiving
        //only needs to check bottom as this forgiving collision only affects the jumping
        foreach (var collide in bottom1_forgive)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                colForgive = true;
            }
        }
        foreach (var collide in bottom1_2_Forgive)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                colForgive = true;
            }
        }
        //if colForgive is found sets forgiving Ground to true allowing player to jump
        if (colForgive == true)
        {
            forgiveGround = true;
            forgiveGround2 = true;
        }
        else
        {
            forgiveGround = false;
            forgiveGround2 = false;
        }
        //this part of the script helps the players become have the same verticality
        //if player1 collides than player2 also collides as well
        if (col == true)
        {
            col2 = true;
            topOrBottom2 = topOrBottom;
            // sets player2 hiddenGround flag for hidden ground animation to play
            hiddenGroundFlag2 = true;
        }
        //if player2 collides than player1 also collides
        else if (col2 == true)
        {
            col = true;
            topOrBottom = topOrBottom2;
            // sets player1 hiddenGround flag for hidden ground animation to play
            hiddenGroundFlag1 = true;
        }
        //if player isn't colliding with anything it is no longer grounded
        if (col == false)
        {
            grounded = false;
            hiddenGroundFlag1 = false;
        }
        if (col2 == false)
        {
            grounded2 = false;
            hiddenGroundFlag2 = false;
        }
        //inverting everything also inverts whether you are hitting the ground or ceiling
        if (inverted == true)
        {
            topOrBottom = topOrBottom * -1;
        }
        if (inverted2 == true)
        {
            topOrBottom2 = topOrBottom2 * -1;
        }
        //if hit a ceiling or bottom of box
        if (col == true && topOrBottom == 1)
        {
            velocity = 0;
        }
        //if hit a ground then sets player to grounded and velocity to 0
        else if (col == true && topOrBottom == -1)
        {
            velocity = 0;
            grounded = true;
        }
        //same for player2
        if (col2 == true && topOrBottom2 == 1)
        {
            velocity2 = 0;
        }
        else if (col2 == true && topOrBottom2 == -1)
        {
            velocity2 = 0;
            grounded2 = true;
        }
        //moves player1
        if (col == false)
        {
            transform.position = transform.position + new Vector3(0, velocity);
        }
        //moves player2
        if (col2 == false)
        {
            otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, velocity2);
        }
        //used to move player up next to collideable object
        if ((col == true && topOrBottom == 1) || (col2 == true && topOrBottom2 == 1))
        {
            float moveDistance = 0f;
            if (col && col2)
            {
                moveDistance = distToCol < distToCol2 ? distToCol : distToCol2;
                transform.position = transform.position + new Vector3(0, moveDistance - .15f);
                otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, moveDistance - .15f);
            }
            else if (col)
            {
                transform.position = transform.position + new Vector3(0, distToCol - .1f);
                otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, distToCol - .1f);
            }
            else
            {
                transform.position = transform.position + new Vector3(0, distToCol2 - .1f);
                otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, distToCol2 - .1f);
            }
        }

        if (topOrBottom == -1)
        {
            if (moving)
            {
                if (!isSet)
                {
                    isSet = true;
                    ghost.transform.position = otherPlayer.transform.position;
                    ghost.transform.SetParent(box.transform);
                }
                transform.SetParent(box.transform);
                otherPlayer.transform.position = new Vector2(otherPlayer.transform.position.x, ghost.transform.position.y);
            }
        }
        else
        {
            isSet = false;
            moving = false;
            transform.SetParent(char_base.transform);
        }

        if (topOrBottom2 == -1)
        {
            if (moving2)
            {
                //t = box.GetComponent<MovingBox>().getT();
                if (!isSet2)
                {
                    isSet2 = true;
                    ghost.transform.position = transform.position;
                    ghost.transform.SetParent(box.transform);
                    //    start = new Vector2(transform.position.x - (box.GetComponent<MovingBox>().xEnd * t), transform.position.y - (box.GetComponent<MovingBox>().yEnd * t));
                    //    end = new Vector2(transform.position.x + (box.GetComponent<MovingBox>().xEnd * t), transform.position.y + (box.GetComponent<MovingBox>().yEnd * t));
                }
                //transform.position = Vector2.Lerp(start, end, t);
                otherPlayer.transform.SetParent(box.transform);
                transform.position = new Vector2(transform.position.x, ghost.transform.position.y);
            }
        }
        else
        {
            isSet2 = false;
            moving2 = false;
            otherPlayer.transform.SetParent(char_base.transform);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gravity")
        {
            invertOnCommand = true;
            Destroy(other.gameObject);
        }
    }

    public void setHiddenGroundFlag1(bool flag)
    {
        this.hiddenGroundFlag1 = flag;
    }
    public bool getHiddenGroundFlag1()
    {
        return hiddenGroundFlag1;
    }

    public void setHiddenGroundFlag2(bool flag)
    {
        this.hiddenGroundFlag2 = flag;
    }

    public bool getHiddenGroundFlag2()
    {
        return hiddenGroundFlag2;
    }

    public int getTopOrBottom()
    {
        return topOrBottom;
    }
    public int getTopOrBottom2()
    {
        return topOrBottom2;
    }

}

