using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBlock : MonoBehaviour
{
    public GameObject player;
    HorizontalMovement horScript;
    bool touchingMoving;
    public float movingPush;
    float width;
    float height;
    public bool leftCol = false;
    public bool rightCol = false;
    bool touchPlayer;

    // Start is called before the first frame update
    void Start()
    {
        horScript = player.GetComponent<HorizontalMovement>();
        touchingMoving = horScript.touchingMoving;
        movingPush = horScript.movingPush;
        width = GetComponent<BoxCollider2D>().bounds.size.x;
        height = GetComponent<BoxCollider2D>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //test colliders of the box's movement to the right
        Collider2D[] colliderRight1 = Physics2D.OverlapCircleAll(transform.position + new Vector3((width / 2) + .05f, 0), 0.01f);
        Collider2D[] colliderRight2 = Physics2D.OverlapCircleAll(transform.position + new Vector3((width / 2) + .05f, (height / 2)), 0.01f);
        Collider2D[] colliderRight3 = Physics2D.OverlapCircleAll(transform.position + new Vector3((width / 2) + .05f, -(height / 2 - .05f)), 0.01f);
        //test colliders of the box's movement to the left
        Collider2D[] colliderLeft1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-(width / 2) - .05f, 0), 0.01f);
        Collider2D[] colliderLeft2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-(width / 2) - .05f, (height / 2)), 0.01f);
        Collider2D[] colliderLeft3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-(width / 2) - .05f, -(height / 2 - .05f)), 0.01f);

        rightCol = false;
        leftCol = false;

        colliderHelper(colliderRight1, true);
        colliderHelper(colliderRight2, true);
        colliderHelper(colliderRight3, true);
        colliderHelper(colliderLeft1, false);
        colliderHelper(colliderLeft2, false);
        colliderHelper(colliderLeft3, false);

        touchingMoving = horScript.touchingMoving;
        Debug.Log("nonPushRight: " + horScript.nonPushLeft);
        if (touchingMoving == true && ((horScript.pushLeft && !horScript.nonPushRight) || (horScript.pushRight && !horScript.nonPushLeft) || (horScript.pushLeft && horScript.nonPushRight && movingPush < 0) || (horScript.pushRight && horScript.nonPushLeft && movingPush > 0))) {
            transform.position += new Vector3(movingPush, 0);
        }
        movingPush = 0;
    }
    private void colliderHelper(Collider2D[] collider, bool onRight)
    {
        foreach (var collide in collider) {
            if (collide.gameObject.GetComponent<Collideable>()) {
                if (onRight)
                {
                    rightCol = true;
                }
                else {
                    leftCol = true;
                }
            }
        }
    }
    }
