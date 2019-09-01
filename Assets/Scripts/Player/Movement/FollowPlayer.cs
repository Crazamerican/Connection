using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    //The two player characters
    public GameObject player;       
    public GameObject player2;

    Camera cam;

    Vector3 playerCam;
    Vector3 player2Cam;

    float height;
    float moveHorizontal;


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        cam = GetComponent<Camera>();
        //the screen position of the two player characters
        playerCam = cam.WorldToScreenPoint(player.transform.position);
        player2Cam = cam.WorldToScreenPoint(player2.transform.position);
        //the screen height
        height = cam.pixelHeight;
    }
    private void Update()
    {
        //the player's horizontal input (whether they are pressing the arrows right or left)
        //0 if not touch, negative if left, positive if right
        moveHorizontal = Input.GetAxis("Horizontal");
        //screen pos of two player characters
        playerCam = cam.WorldToScreenPoint(player.transform.position);
        player2Cam = cam.WorldToScreenPoint(player2.transform.position);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //avg screen x of two players
        float playerAvg = (playerCam.x + player2Cam.x) / 2;
        Vector3 camDif = new Vector3(0, 0, 0);
        Vector3 camLeft = new Vector3(0, 0, 0);
        //gets the very lefthand side of the screen in the world position
        camLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        //if moving right
        if (moveHorizontal > 0)
        {
            if (playerAvg > cam.pixelWidth / 2)
            {
                //make camDif the playerAvg if playerAvg is bigger than middle of screen
                camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg, height / 2));
                //move cam to new pos
                transform.position = camDif;
            }
        }
        //if moving left and left side of screen is greater than -18 (which is the very left side of the level) 
        else if (moveHorizontal < 0 && camLeft.x >= -18)
        {
            if (playerAvg < cam.pixelWidth / 2)
            {
                camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg, height / 2));
                transform.position = camDif;
            }
        }
        else if (moveHorizontal == 0 && camLeft.x >= -18) {
            camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg, height / 2));
            transform.position = camDif;
        }
        //transform.position = player.transform.position + offset;
    }
}
