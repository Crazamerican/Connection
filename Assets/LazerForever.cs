using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LazerForever : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D col;
    public Transform lazerHit;
    public Transform startLoc;
    public Transform endLoc;
    public bool on;
    private bool changing;
    public float size = .5f;
    private bool changeOn;
    private bool changeOff;
    private float timeRemaining;
    private float changeTime;
    private float damageTime;
    private Gradient grad;
    public Color fadeIn;
    public Color fullColor;
    public int delay;
    public float holdTime = 2;
    public float offTime = 2;
    float timer;
    float changeTimer;
    public GameObject Checkpoint;
    public GameObject player1;
    public GameObject player2;
    CheckpointScript checkpointScript;

    public GameObject playBoth;
    DeathScript deathScript;

    // Start is called before the first frame update
    void Start()
    {
        deathScript = playBoth.GetComponent<DeathScript>();
        checkpointScript = Checkpoint.GetComponent<CheckpointScript>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        on = true;
        changing = false;
        changeTime = 0 + delay;
        //Debug.Log("The starting change time is: " + changeTime);
        col = GetComponent<BoxCollider2D>();
        changeOn = false;
        changeOff = false;
        damageTime = 0f;
        timer = 0f;
        changeTimer = 0;
        if (holdTime < 2)
        {
            holdTime = 2;
        }
        if (offTime < 2)
        {
            offTime = 2f;
        }
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 midPoint = new Vector2((startLoc.position.x + lazerHit.position.x) / 2, (startLoc.position.y + lazerHit.position.y) / 2);
        transform.position = midPoint;
        float distance = Mathf.Sqrt(Mathf.Pow(lazerHit.position.x - startLoc.position.x, 2) + Mathf.Pow(lazerHit.position.y - startLoc.position.y, 2));
        float angle = Mathf.Rad2Deg * Mathf.Atan2((startLoc.position.y - lazerHit.position.y), (startLoc.position.x - lazerHit.position.x));
        if (angle < 0)
        {
            angle += 360;
        }
        angle = angle + 90;
        //Debug.Log("the angle is: " + angle);
        float sizeX = Mathf.Cos(angle) * distance;
        float sizeY = Mathf.Sin(angle) * distance;
        col.size = new Vector2(size, distance);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        timer += Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(startLoc.position, endLoc.position - startLoc.position);
        //Debug.DrawLine(startLoc.position, hit.point);
        lineRenderer.positionCount = 2;
        lazerHit.position = hit.point;
        lineRenderer.SetPosition(0, startLoc.position);
        lineRenderer.SetPosition(1, lazerHit.position);

        if (on && hit.collider != null && (hit.collider.gameObject.name.Equals("Player") || hit.collider.gameObject.name.Equals("Player2")))
        {
            deathScript.dead = true;
        }
        if (changeTime < timer && !changeOn)
        {
            //Debug.Log("Turning On");
            changeOn = true;
            changeTime = timer + 15f;
        }
        if (on && timeRemaining < timer)
        {
            changeOff = true;
        }
        if (changeOn)
        {
            changeTimer += Time.deltaTime;
            startChanging();
        }
        /*
        if (changeOff)
        {
            changeTimer += Time.deltaTime;
            reverseChanging();
        }
        */
    }

    void turnOn()
    {
        on = true;
        lineRenderer.enabled = true;
        changing = true;
        timeRemaining = timer + 5f;

    }

    void turnOff()
    {
        on = false;
        lineRenderer.enabled = false;
    }

    void startChanging()
    {
        if (lineRenderer.enabled != true)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.startColor = Color.Lerp(fadeIn, fullColor, Mathf.PingPong(changeTimer, 2));
        lineRenderer.endColor = Color.Lerp(fadeIn, fullColor, Mathf.PingPong(changeTimer, 2));
        if (lineRenderer.startColor.a > .95f)
        {
            lineRenderer.startColor = fullColor;
            lineRenderer.endColor = fullColor;
            changeOn = false;
            on = true;
            timeRemaining = timer + holdTime;
            changeTimer = 0;

        }
    }

    void reverseChanging()
    {
        lineRenderer.startColor = Color.Lerp(fullColor, fadeIn, Mathf.PingPong(changeTimer, 2));
        lineRenderer.endColor = Color.Lerp(fullColor, fadeIn, Mathf.PingPong(changeTimer, 2));
        if (on)
        {
            on = !on;
        }
        if (lineRenderer.startColor.a < .05f)
        {
            lineRenderer.startColor = fadeIn;
            lineRenderer.endColor = fadeIn;
            changeOff = false;
            lineRenderer.enabled = false;
            changeTime = timer + offTime;
            changeTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.transform.position);

        if (on && (col.gameObject.name.Equals("Player") || col.gameObject.name.Equals("Player2")))
        {
            deathScript.dead = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(col.transform.position);

        if (on && (col.gameObject.name.Equals("Player") || col.gameObject.name.Equals("Player2")))
        {
            deathScript.dead = true;
        }
    }
}

