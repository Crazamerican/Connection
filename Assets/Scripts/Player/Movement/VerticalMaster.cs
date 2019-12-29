using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMaster : MonoBehaviour
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    [SerializeField] private bool grounded;
    [SerializeField] private bool grounded2;
    private float velocity;
    private float velocity2;
    public float gravity;
    public float gravity2;
    public GameObject otherPlayer;
    private bool onBox;
    private bool onBox2;
    //1 is top 0 is nothing -1 is bottom
    private int topOrBottom;
    private int topOrBottom2;
    float distToCol;
    float distToCol2;

    private bool inverted;
    private bool inverted2;
    private bool inverted2_2;
    private bool invertOnCommand;
    VerticalOther otherScript;
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

    bool forgiveGround;
    bool forgiveGround2;

    // Use this for initialization
    void Start()
    {
        cameraScript = cameraBoi.GetComponent<FollowPlayer>();
        Application.targetFrameRate = 60;
        grounded = true;
        grounded2 = true;
        forgiveGround = true;
        forgiveGround2 = true;
        onBox = false;
        onBox2 = false;
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
        
    }

    private void Update()
    {
        //if jump button pressed and a character is on the ground
        if (Input.GetButtonDown("Jump") && (forgiveGround || forgiveGround2) && cameraScript.freezePlayers == false)
        {
            if (inverted == true || inverted2 == true)
            {
                velocity = -jumpTakeOffSpeed;
                velocity2 = -jumpTakeOffSpeed;
            } else
            {
                velocity = jumpTakeOffSpeed;
                velocity2 = jumpTakeOffSpeed;
            }
            grounded = false;
            grounded2 = false;
            forgiveGround = false;
            forgiveGround2 = false;
            onBox = false;
            onBox2 = false;
            audioSource.PlayOneShot(jumpSound, 0.7F);
            
        }
        if (Input.GetButtonDown("Invert") && invertOnCommand == true && (grounded || grounded2) && cameraScript.freezePlayers == false)
        {
            inverted2 = !inverted2;
            inverted = !inverted;
            gravity2 = gravity2 * -1;
            gravity = gravity * -1;
        }
        if (grounded || grounded2)
        {
            charAnim.SetTrigger("grounded");
            otherCharAnim.SetTrigger("grounded");
        } 

        
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        moving = false;

        if (inverted2 != otherScript.inverted2 && inverted2_2 == false)
        {
            /*inverted2 = otherScript.inverted2;
            inverted = !inverted;
            gravity2 = gravity2 * -1;
            gravity = gravity * -1;*/
            invertOnCommand = true;
        }
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
        Collider2D[] top1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(width / 2, velocity + height / 2), 0.01f);
        Collider2D[] top2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-width / 2, velocity + height / 2), 0.01f);
        Collider2D[] top3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, velocity + height / 2), 0.01f);
        Collider2D[] bottom1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(width / 2, velocity - height / 2), 0.01f);
        Collider2D[] bottom2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-width / 2, velocity - height / 2), 0.01f);
        Collider2D[] bottom3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, velocity - height / 2), 0.01f);
        Collider2D[] bottom1_forgive = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity - height / 2), transform.position + new Vector3(+width / 2, velocity - height / 2 - .5f));
        Collider2D[] top1_2 = otherScript.GetTop1(velocity2);
        Collider2D[] top2_2 = otherScript.GetTop2(velocity2);
        Collider2D[] top3_2 = otherScript.GetTop3(velocity2);
        Collider2D[] bottom1_2 = otherScript.GetBot1(velocity2);
        Collider2D[] bottom2_2 = otherScript.GetBot2(velocity2);
        Collider2D[] bottom3_2 = otherScript.GetBot3(velocity2);
        Collider2D[] bottom1_2_Forgive = otherScript.GetBot_Forgive(velocity2);

        charAnim.SetFloat("verticalSpeed", velocity);
        otherCharAnim.SetFloat("verticalSpeed", velocity);

        bool col = false;
        bool col2 = false;

        bool colForgive = false;

        distToCol = Mathf.Infinity;
        distToCol2 = Mathf.Infinity;

        foreach (var collide in top1)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                topOrBottom = 1;
                distToCol = GetComponent<BoxCollider2D>().Distance(collide).distance;
                if (collide.tag == "Ground")
                {
                    onBox = false;
                }
                else
                {
                    onBox = true;
                }
                //if (collide.gameObject.GetComponent<MovingBox>())
                //{
                //    moving = true;
                //    box = collide.gameObject;
                //    Debug.Log("hit");
                //}
            }
        }
        foreach (var collide in top2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col = true;
                topOrBottom = 1;
                distToCol = GetComponent<BoxCollider2D>().Distance(collide).distance;
                if (collide.tag == "Ground")
                {
                    onBox = false;
                }
                else
                {
                    onBox = true;
                }
                //if (collide.gameObject.GetComponent<MovingBox>())
                //{
                //    moving = true;
                //    box = collide.gameObject;
                //    Debug.Log("hit");
                //}
            }
        }
        foreach (var collide in top3)
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
                //if (collide.gameObject.GetComponent<MovingBox>())
                //{
                //    moving = true;
                //    box = collide.gameObject;
                //    Debug.Log("hit");
                //}
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
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving = true;
                    box = collide.gameObject;
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
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving = true;
                    box = collide.gameObject;
                }
            }
        }
        foreach (var collide in bottom3)
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
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving = true;
                    box = collide.gameObject;
                }
            }
        }
        foreach (var collide in top1_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                distToCol2 = otherPlayer.GetComponent<BoxCollider2D>().Distance(collide).distance;
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
                //if (collide.gameObject.GetComponent<MovingBox>())
                //{
                //    moving2 = true;
                //    box = collide.gameObject;
                //}
            }
        }
        foreach (var collide in top2_2)
        {
            if (collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground")
            {
                col2 = true;
                topOrBottom2 = 1;
                distToCol2 = otherPlayer.GetComponent<BoxCollider2D>().Distance(collide).distance;

                if (collide.tag == "Ground")
                {
                    onBox2 = false;
                }
                else
                {
                    onBox2 = true;
                }
                //if (collide.gameObject.GetComponent<MovingBox>())
                //{
                //    moving2 = true;
                //    box = collide.gameObject;
                //}
            }
        }
        foreach (var collide in top3_2)
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
                //if (collide.gameObject.GetComponent<MovingBox>())
                //{
                //    moving2 = true;
                //    box = collide.gameObject;
                //}
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
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving2 = true;
                    box = collide.gameObject;
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
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving2 = true;
                    box = collide.gameObject;
                }
            }
        }
        foreach (var collide in bottom3_2)
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
                if (collide.gameObject.GetComponent<MovingBox>())
                {
                    moving2 = true;
                    box = collide.gameObject;
                }
            }
        }
        if (grounded || grounded2)
        {
            velocity = 0;
            velocity2 = 0;
        }
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
        if (colForgive == true)
        {
            forgiveGround = true;
            forgiveGround2 = true;
        } else
        {
            forgiveGround = false;
            forgiveGround2 = false;
        }
        Debug.Log(forgiveGround);
        Debug.Log(forgiveGround2);
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
        if (inverted == true)
        {
            topOrBottom = topOrBottom * -1;
        }
        if (inverted2 == true)
        {
            topOrBottom2 = topOrBottom2 * -1;
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
        
        if ((col == true && topOrBottom == 1) || (col2 == true && topOrBottom2 == 1))
        {
            Debug.Log("col1 = " + col);
            Debug.Log("col2 = " + col2);
            float moveDistance = 0f;
            if (col && col2)
            {
                moveDistance = distToCol < distToCol2 ? distToCol : distToCol2;
                transform.position = transform.position + new Vector3(0, moveDistance - .15f);
                otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, moveDistance - .15f);
            } else if (col)
            {
                transform.position = transform.position + new Vector3(0, distToCol - .1f);
                otherPlayer.transform.position = otherPlayer.transform.position + new Vector3(0, distToCol - .1f);
            } else
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
        

        //if (topOrBottom == 1 || topOrBottom2 == 1)
        //{
        //if (moving || moving2)
        //{
        //box.GetComponent<MovingBox>().stopMoving();
        //}
        //}

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gravity")
        {
            /*inverted = !inverted;
            inverted2 = !inverted2;
            inverted2_2 = !inverted2_2;
            gravity = gravity * -1;
            gravity2 = gravity2 * -1;*/
            invertOnCommand = true;
            Destroy(other.gameObject);
        }
    }

}

