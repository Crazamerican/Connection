using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel_V2 : MonoBehaviour
{
    public float speed = .03f;
    float curSpeed = 0;
    public GameObject player;
    public GameObject player2;
    VerticalMaster verticalMaster;
    HorizontalMovement horizontal;
    public int direction = 0;
    float oldSpeed;
    // Start is called before the first frame update
    void Start()
    {
        verticalMaster = player.GetComponent<VerticalMaster>();
        horizontal = player.GetComponent<HorizontalMovement>();
        oldSpeed = horizontal.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //direction is left
        if (direction == 1) {
            Vector3 transPos = new Vector3(-curSpeed, 0, 0);
            player.transform.position = player.transform.position + transPos;
        }
        //direction is right
        if (direction == 2) {
            Vector3 transPos = new Vector3(curSpeed, 0, 0);
            player.transform.position = player.transform.position + transPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            //direction is up
            if (direction == 0) {
                curSpeed = speed;
                verticalMaster.inTunnel = true;
            }
            //direction is left
            if (direction == 1) {
                curSpeed = speed;
            }
            //direction is right
            if (direction == 2)
            {
                curSpeed = speed;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //direction is up
            if (direction == 0) {
                curSpeed = 0.0f;
                verticalMaster.inTunnel = false;
            }
            //direction is left
            if (direction == 1) {
                curSpeed = 0.0f;
            }
            //direction is right
            if (direction == 2) {
                curSpeed = 0.0f;
            }
        }
    }
}
