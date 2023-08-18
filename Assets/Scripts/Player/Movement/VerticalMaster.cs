using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMaster : MonoBehaviour
{
    //speed players initial jump is set to affects jump height as well
    public float jumpTakeOffSpeed = 7;

    public bool grounded;
    public bool grounded2;
    //player current speed
    [SerializeField] public float velocity;
    [SerializeField] public float velocity2;
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
    public bool inverted;
    public bool inverted2;
    private bool inverted2_2;
    private bool invertOnCommand;
    //script to grab parameters from Player2
    VerticalOther otherScript;
    //the width and height of Player1
    float width;
    float height;
    //float width2;
    //float height2;

    public Animator charAnim;
    public Animator otherCharAnim;

    public PlayerSFX playerSFXController;
    public AudioClip jumpSound;
    AudioSource audioSource;

    /*bool moving;
    bool isSet;

    bool moving2;
    bool isSet2;

    GameObject box;
    public GameObject char_base;
    public GameObject ghost;*/

    private GameManagementScript gameManagement;

    //similar to grounded but with a bit of extra wiggle room so player can be near ground and still jump
    public bool forgiveGround;
    public bool forgiveGround2;

    bool floatTop;
    int floatTimer;

    public bool inTunnel;

    public bool onMoving;
    public float onSpeed;
    public bool onMoving2;
    public float onSpeed2;

    private bool hiddenGroundFlag1;
    private bool hiddenGroundFlag2;

    bool player2OnBottom;
    bool player1NearGround;
    bool player2NearGround;

    bool topHold;
    int topTimer;

    public bool coyoteGround;
    public bool coyoteGround2;
    int coyoteTimer;
    int coyoteTimer2;

    public bool changeGravity;
    int changeGravityFreeze;

    PauseMenuController pauseScript;

    // Use this for initialization
    void Start()
    {
        pauseScript = GameObject.Find("Canvas").GetComponent<PauseMenuController>();
        gameManagement = GameObject.Find("EndOfLevel").GetComponent<GameManagementScript>();
        changeGravityFreeze = 0;
        changeGravity = false;
        coyoteTimer = 0;
        coyoteTimer2 = 0;
        coyoteGround = false;
        coyoteGround2 = false;
        topTimer = 0;
        topHold = false;
        player1NearGround = false;
        player2NearGround = false;
        player2OnBottom = false;
        onMoving = false;
        onSpeed = 0f;
        onMoving2 = false;
        onSpeed2 = 0f;
        inTunnel = false;
        floatTimer = 0;
        floatTop = false;
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
        /*moving = false;
        isSet = false;
        moving2 = false;
        isSet2 = false;
        box = null;*/
        hiddenGroundFlag1 = false;
        hiddenGroundFlag2 = false;

    }

    private void Update()
    {
        if (coyoteGround == true)
        {
            coyoteTimer++;
            if (coyoteTimer >= 7)
            {
                coyoteGround = false;
                coyoteTimer = 0;
            }
        }
        if (coyoteGround2 == true)
        {
            coyoteTimer2++;
            if (coyoteTimer2 >= 7)
            {
                coyoteGround2 = false;
                coyoteTimer2 = 0;
            }
        }
        //if jump button pressed and a character is on or extremely near the ground and not frozen
        if (Input.GetButtonDown("Jump") && !pauseScript.isPaused && changeGravity == false && (forgiveGround || forgiveGround2 || coyoteGround || coyoteGround2 || grounded || grounded2) && gameManagement.freezePlayer == false && (!otherPlayer.GetComponent<HorizontalMovement>().touchingMoving && !this.GetComponent<HorizontalMovement>().touchingMoving))
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
            coyoteGround = false;
            coyoteGround2 = false;
            playerSFXController.playPlayerSFX(PlayerSFX.ClipName.JUMP);
            //audioSource.PlayOneShot(jumpSound, 0.7F);

            charAnim.ResetTrigger("grounded");
            otherCharAnim.ResetTrigger("grounded");

            charAnim.SetTrigger("jumped");
            otherCharAnim.SetTrigger("jumped");
        }
        //if gotten gravity inverting powerup, user can invert gravity while grounded
        if (Input.GetButtonDown("Invert") && invertOnCommand == true && (grounded || grounded2) && gameManagement.freezePlayer == false)
        {
            inverted2 = !inverted2;
            inverted = !inverted;
            gravity2 = gravity2 * -1;
            gravity = gravity * -1;
            if (inverted) {
                changeGravity = true;
                velocity = 0;
                velocity2 = 0;
            }
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Debug.Log("player.y = " + transform.position.y + "player2.y = " + otherPlayer.transform.position.y);
        player1NearGround = false;
        player2NearGround = false;
        player2OnBottom = false;
        onMoving = false;
        onSpeed = 0f;
        onMoving2 = false;
        onSpeed2 = 0f;
        //moving = false;
        
        /*if (inverted2 != otherScript.inverted2 && inverted2_2 == false)
        {
            //invertOnCommand = true;
        }*/
        topOrBottom = 0;
        topOrBottom2 = 0;
        if (changeGravity == true) {
            velocity = 0;
            velocity2 = 0;
            if (changeGravityFreeze <= 4)
            {
                velocity = velocity - (gravity);
                velocity2 = velocity2 - (gravity2);
                changeGravity = false;
                changeGravityFreeze = 0;
            }
            changeGravityFreeze++;
        }
        //once at height of jump, hovers for 5 frames
        else if (velocity > 0 && velocity - gravity < 0 || floatTop == true)
        {
            velocity = 0;
            velocity2 = 0;
            floatTop = true;
            if (floatTimer > 8)
            {
                floatTop = false;
                floatTimer = 0;
            }
            floatTimer++;
        }
        else if (topHold == true)
        {
            velocity = 0;
            velocity2 = 0;
            if (topTimer > 5)
            {
                topHold = false;
                topTimer = 0;
            }
            topTimer++;
        } //if in upward windtunnel sets velocity to .15f
        else if (inTunnel == true)
        {
            velocity = 0.15f;
            velocity2 = 0.15f;
        } 
        else if (grounded || grounded2)
        {
            velocity = 0f;
            velocity2 = 0f;
        } //incorperates velocity to gravity
        else
        {
            velocity = velocity - gravity;
            velocity2 = velocity2 - gravity2;
        }
        //sets a bunch of colliders based on where the player will be if you incorperate the velocity
        Collider2D[] topboi = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2.1f, velocity + height / 2), transform.position + new Vector3(+width / 2.1f, velocity + height / 2 + .01f));
        Collider2D[] bottomboi = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2.1f, velocity - height / 2), transform.position + new Vector3(+width / 2.1f, velocity - height / 2 - .01f));
        Collider2D[] bottomboi_Moving = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2.1f, velocity - height / 2), transform.position + new Vector3(+width / 2.1f, velocity - height / 2 - .01f));
        //invert gravity
        if (inverted) {
            bottomboi_Moving = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2.1f, velocity + height / 2), transform.position + new Vector3(+width / 2.1f, velocity + height / 2 + .01f));
        }
        //forgiving collider goes a bit further than regular colliders
        Collider2D[] bottom1_forgive = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2.1f, velocity - height / 2), transform.position + new Vector3(+width / 2.1f, velocity - height / 2 - .3f));
        //player2 colliders from VerticalOther script
        Collider2D[] topboi_2 = otherScript.GetTopBoi(velocity2);
        Collider2D[] bottomboi_2 = otherScript.GetBotBoi(velocity2);
        Collider2D[] bottomboi_2_Moving = otherScript.GetBotBoi_Moving(velocity2);
        Collider2D[] bottom1_2_Forgive = otherScript.GetBot_Forgive(velocity2);

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
                float newDist = GetComponent<BoxCollider2D>().Distance(collide).distance;
                if (newDist < distToCol) {
                    distToCol = newDist;
                }
            }
        }
        //see if player1 is colliding with something on it's bottom
        foreach (var collide in bottomboi)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                //Debug.Log("bottom player1");
                col = true;
                topOrBottom = -1;
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    //moving = true;
                    //box = collide.gameObject;
                }

                float newDist = GetComponent<BoxCollider2D>().Distance(collide).distance;
                if (newDist < distToCol)
                {
                    distToCol = newDist;
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
                float newDist = otherPlayer.GetComponent<BoxCollider2D>().Distance(collide).distance;
                if (newDist < distToCol2)
                {
                    distToCol2 = newDist;
                }
                col2 = true;
                topOrBottom2 = 1;
            }
        }

        foreach (var collide in bottomboi_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                //Debug.Log("bottom player2");
                col2 = true;
                topOrBottom2 = -1;
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    //moving2 = true;
                    //box = collide.gameObject;
                }
                float newDist = otherPlayer.GetComponent<BoxCollider2D>().Distance(collide).distance;
                if (newDist < distToCol2)
                {
                    distToCol2 = newDist;
                }
            }
            if (collide.tag == "Ground")
            {
                player2OnBottom = true;
            }
        }

        foreach (var collide in bottomboi_2_Moving)
        {
            if (collide.gameObject.tag == "MoveBox")
            {
                //Debug.Log("onMovingBox");
                onMoving2 = true;
                onSpeed2 = collide.gameObject.GetComponent<HorizontalBox>().speed;
            }
        }

        
        //same collider but with the forgiving
        //only needs to check bottom as this forgiving collision only affects the jumping
        foreach (var collide in bottom1_forgive)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                colForgive = true;
                player1NearGround = true;
            }
        }
        foreach (var collide in bottom1_2_Forgive)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                colForgive = true;
                player2NearGround = true;
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
            if (col2 == false && player2NearGround == false)
            {
                // sets player2 hiddenGround flag for hidden ground animation to play
                hiddenGroundFlag2 = true;
            }


            //col2 = true;
            topOrBottom2 = topOrBottom;
        }
        //if player2 collides than player1 also collides
        else if (col2 == true)
        {
            if (col == false && player1NearGround == false)
            {
                // sets player1 hiddenGround flag for hidden ground animation to play
                if (player2OnBottom == false)
                {
                    hiddenGroundFlag1 = true;
                }
            }
            
            //col = true;

            topOrBottom = topOrBottom2;
        }


        //if player isn't colliding with anything it is no longer grounded
        if (col == false)
        {
            if (grounded == true)
            {
                coyoteGround = true;
            }
            grounded = false;
            //hiddenGroundFlag1 = false;
        }
        if (col2 == false)
        {
            if (grounded2 == true)
            {
                coyoteGround2 = true;
            }
            grounded2 = false;
            //hiddenGroundFlag2 = false;
        }
        if (!grounded && !grounded2) {
            hiddenGroundFlag1 = false;
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

        if (!grounded && (col || col2))
        {
            FlushCollision(col, col2, topOrBottom, topOrBottom2);
        }
        else if (col || col2) {
            //Debug.Log("hit collision, topOrBottom: " + topOrBottom + " topOrBottom2: " + topOrBottom2);
            //FlushCollision(col, col2, topOrBottom, topOrBottom2);
        }

        //if hit a ceiling or bottom of box
        if (col == true && topOrBottom == 1)
        {
            //Debug.Log("ceiling?");
            velocity = 0;
            grounded = false;
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
            grounded = false;
        }
        else if (col2 == true && topOrBottom2 == -1)
        {


            velocity2 = 0;
            grounded2 = true;
        }

        //moves player1
        if (col == false && col2 == false)
        {
            if (Mathf.Abs(velocity) > .18f && inverted == true) {
                if (velocity > 0)
                {
                    velocity = .18f;
                    velocity2 = .18f;
                }
            }
            transform.position = transform.position + new Vector3(0, velocity, 0);
            otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, velocity2, 0);
        }
        
        //used to move player up next to collideable object
        if ((col == true && topOrBottom == 1) || (col2 == true && topOrBottom2 == 1))
        {
            topHold = true;
            charAnim.SetTrigger("bonk");
            otherCharAnim.SetTrigger("bonk");

            //FlushCollision(col, col2);
        }

        /*if (topOrBottom == -1)
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
                otherPlayer.transform.position = new Vector3(otherPlayer.transform.position.x, ghost.transform.position.y, -1);
            }
        }
        else
        {
            isSet = false;
            moving = false;
            //transform.SetParent(char_base.transform);
        }*/

        /*if (topOrBottom2 == -1)
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
            //otherPlayer.transform.SetParent(char_base.transform);
        }*/

        //if either player is on ground, then stops player from moving vertically.
        if (grounded || grounded2)
        {
            charAnim.SetTrigger("grounded");
            otherCharAnim.SetTrigger("grounded");
        } else
        {
            charAnim.ResetTrigger("grounded");
            otherCharAnim.ResetTrigger("grounded");
        }

        charAnim.SetFloat("verticalSpeed", velocity);
        otherCharAnim.SetFloat("verticalSpeed", velocity2);

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

    private void FlushCollision(bool col, bool col2, int topbottom1, int topbottom2)
    {
        Debug.Log("FlushCollision");
        float direction = -1f;
        float padding = -.015f;
        //determine proper direction for correction shift
        if (inverted)
        {
            direction *= -1f;
            padding = .007f;
        }

        if (topbottom1 == 1 || topbottom2 == 1)
        {
            padding = .011f;
        }

        if (col && col2)
        {
            //Debug.Log("In collision player.y = " + transform.position.y + "player2.y = " + otherPlayer.transform.position.y);
            float moveDistance = 0f;
            moveDistance = distToCol < distToCol2 ? distToCol : distToCol2;
            int correctTopBottom = distToCol < distToCol2 ? topbottom1 : topbottom2;
            //Debug.Log(moveDistance);
            transform.position = transform.position + new Vector3(0, (moveDistance - padding) * direction * -correctTopBottom);
            otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, (moveDistance - padding) * direction * -correctTopBottom);
        }
        else if (col)
        {
            transform.position = transform.position + new Vector3(0, (distToCol - padding) * direction * -topbottom1);
            otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, (distToCol - padding) * direction * -topbottom1);
        }
        else
        {
            transform.position = transform.position + new Vector3(0, (distToCol2 - padding) * direction * -topbottom2);
            otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, (distToCol2 - padding) * direction * -topbottom2);
        }
    }
}