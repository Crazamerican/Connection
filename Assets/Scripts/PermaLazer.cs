using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PermaLazer : MonoBehaviour
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
        lineRenderer.enabled = true;
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
        if(col == null)
        {
            col = GetComponent<BoxCollider2D>();
        }
        if(lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        col.size = new Vector2(size, distance);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        RaycastHit2D hit = Physics2D.Raycast(startLoc.position, endLoc.position - startLoc.position);
        //Debug.DrawLine(startLoc.position, hit.point);
        lineRenderer.positionCount = 2;
        lazerHit.position = hit.point;
        lineRenderer.SetPosition(0, startLoc.position);
        lineRenderer.SetPosition(1, lazerHit.position);
        lineRenderer.endColor = fullColor;
        lineRenderer.startColor = fullColor;

        if (on && hit.collider != null && (hit.collider.gameObject.name.Equals("Player") || hit.collider.gameObject.name.Equals("Player2")))
        {
            deathScript.dead = true;
        }
    }

    void turnOn()
    {
        on = true;
        lineRenderer.enabled = true;
        changing = true;

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
            changeTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (on && (col.gameObject.name.Equals("Player") || col.gameObject.name.Equals("Player2")))
        {
            if(deathScript == null)
            {
                deathScript = playBoth.GetComponent<DeathScript>();
            }
            deathScript.dead = true;
        }
    } 

    void OnTriggerStay2D(Collider2D col)
    {

        if (on && (col.gameObject.name.Equals("Player") || col.gameObject.name.Equals("Player2")))
        {
            if (deathScript == null)
            {
                deathScript = playBoth.GetComponent<DeathScript>();
            }
            deathScript.dead = true;
        }
    }
}

