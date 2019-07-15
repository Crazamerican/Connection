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

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        on = false;
        changing = false;
        changeTime = 0;
        changeOn = false;
        changeOff = false;
        damageTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(startLoc.position, endLoc.position - startLoc.position);
        Debug.DrawLine(startLoc.position, hit.point);
        lineRenderer.positionCount = 2;
        lazerHit.position = hit.point;
        lineRenderer.SetPosition(0, startLoc.position);
        lineRenderer.SetPosition(1, lazerHit.position);
        //Debug.Log(hit.collider.gameObject.name);
        if (on && (hit.collider.gameObject.name.Equals("Player") || hit.collider.gameObject.name.Equals("Player2")))
        {
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log(damageTime);
            if (damageTime < Time.time)
            {
                hit.collider.gameObject.GetComponentInParent<Health>().takeDamage(1);
                Debug.Log("Here is where the damage will go");
                damageTime = Time.time + .15f;
            }
        }
        if(changeTime < Time.time)
        {
            changeOn = true;
            changeTime = Time.time + 15f;
        }
        if (on && timeRemaining < Time.time)
        {
            changeOff = true;
        }
        if (changeOn)
        {
            startChanging();
        }
        if(changeOff)
        {
            reverseChanging();
        }
        


    }

    void turnOn()
    {
        on = true;
        lineRenderer.enabled = true;
        changing = true;
        timeRemaining = Time.time + 5f;

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
        lineRenderer.startColor = Color.Lerp(fadeIn, fullColor, Mathf.PingPong(Time.time, 1));
        lineRenderer.endColor = Color.Lerp(fadeIn, fullColor, Mathf.PingPong(Time.time, 1));
        if (lineRenderer.startColor.a > .95f)
        {
            changeOn = false;
            on = true;
            timeRemaining = Time.time + 1.5f;

        }
    }

    void reverseChanging()
    {
        lineRenderer.startColor = Color.Lerp(fullColor, fadeIn, Mathf.PingPong(Time.time, 1));
        lineRenderer.endColor = Color.Lerp(fullColor, fadeIn, Mathf.PingPong(Time.time, 1));
        if (lineRenderer.startColor.a < .05f)
        {
            changeOff = false;
            lineRenderer.enabled = false;
            changeTime = Time.time + 3f;
            on = false;
        }
    }

}
