using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompyBoiScript : MonoBehaviour
{
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private int state; //State Machine States: 0 goes left, 1 goes right, 2 drops on player
    public float moveSpeed = 2.0f;
    public float attackSpeed = 3.5f;
    private float moveNormalizer;
    private bool up;
    private Vector3 helperPosition;
    float lerpTick;
    float timer;
    SpriteRenderer rend;
    float cdTimer;
    Color startColor = Color.white;
    Color angryColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;//Go left first
        up = false;
        moveNormalizer = .005f;
        leftPoint = transform.position + Vector3.left * 5;
        rightPoint = transform.position + Vector3.right * 5;
        lerpTick = 0;
        timer = 0;
        rend = GetComponent<SpriteRenderer>();
        cdTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        thrompCheck();
        if(state==0)//Left Movement
        {
            if(rend.color != startColor)
            {
                rend.color = startColor;
            }
            transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
            if(Mathf.Abs(transform.position.x - leftPoint.x)< .05)
            {
                lerpTick = 0;
                state = 1;
            }
        }
        else if (state==1)//Right Movement
        {
            transform.position = transform.position + Vector3.right * moveSpeed * Time.deltaTime;
            if (Mathf.Abs(transform.position.x - rightPoint.x) < .05)
            {
                lerpTick = 0;
                state = 0;
            }
        }
        else if(state==2) //state==2, Thromping
        {
            if(up)
            {
                timer = Time.time + 0.5f;
                up = false;
            }
            else
            {
                if(timer > Time.time)
                {
                    transform.position = transform.position + Vector3.up * moveSpeed * .5f * Time.deltaTime;
                    rend.color = Color.Lerp(startColor, angryColor, Mathf.PingPong(Time.time, 1));
                }
                else
                {
                    rend.color = angryColor;
                    transform.position = transform.position + Vector3.down * attackSpeed * Time.deltaTime;
                }
            }
        }
        else //Returning to position
        {
            transform.position = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
            rend.color = Color.Lerp(angryColor, startColor, Mathf.PingPong(Time.time, 1));
            if (Mathf.Abs(transform.position.y - helperPosition.y) < .05f)
            {
                transform.position = helperPosition;
                state = 0;
            }
        }
    }

    void thrompCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if(hit.collider != null && cdTimer < Time.time)
        {
            if(hit.collider.gameObject.tag.Equals("Player"))
            {
                cdTimer = Time.time + 5.0f;
                state = 2;
                helperPosition = transform.position;
                up = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            state = 3;
            Debug.Log("Collision hit with: " + collision.gameObject.name);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            state = 3;
            Debug.Log("Trigger hit with: " + collision.gameObject.name);
        }
    }
}
