using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform lazerHit;
    public Transform startLoc;
    public Transform endLoc;
    private bool on;
    private bool changing;
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

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        on = false;
        changing = false;
        changeTime = 0 + delay;
        Debug.Log("The starting change time is: " + changeTime);
        changeOn = false;
        changeOff = false;
        damageTime = 0f;
        timer = 0f;
        changeTimer = 0;
        if(holdTime < 2)
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
        timer += Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(startLoc.position, endLoc.position - startLoc.position);
        Debug.DrawLine(startLoc.position, hit.point);
        lineRenderer.positionCount = 2;
        lazerHit.position = hit.point;
        lineRenderer.SetPosition(0, startLoc.position);
        lineRenderer.SetPosition(1, lazerHit.position);
        //Debug.Log(hit.collider.gameObject.name);
        if (on && (hit.collider.gameObject.name.Equals("Player") || hit.collider.gameObject.name.Equals("Player2")))
        {
            
        }
        if(changeTime < timer && !changeOn)
        {
            Debug.Log("Turning On");
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
        if(changeOff)
        {
            changeTimer += Time.deltaTime;
            reverseChanging();
        }
        


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
        if(lineRenderer.enabled != true)
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
        if (lineRenderer.startColor.a < .05f)
        {
            lineRenderer.startColor = fadeIn;
            lineRenderer.endColor = fadeIn;
            changeOff = false;
            lineRenderer.enabled = false;
            changeTime = timer + offTime;
            on = false;
            changeTimer = 0;
        }
    }

}
