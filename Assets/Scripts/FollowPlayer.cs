using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
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
        playerCam = cam.WorldToScreenPoint(player.transform.position);
        player2Cam = cam.WorldToScreenPoint(player2.transform.position);
        height = cam.pixelHeight;
    }
    private void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        playerCam = cam.WorldToScreenPoint(player.transform.position);
        player2Cam = cam.WorldToScreenPoint(player2.transform.position);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        float playerAvg = (playerCam.x + player2Cam.x) / 2;
        Vector3 camDif = new Vector3(0, 0, 0);
        Vector3 camLeft = new Vector3(0, 0, 0);
        camLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        if (moveHorizontal > 0)
        {
            if (playerAvg > cam.pixelWidth / 2)
            {
                camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg, height / 2));
                transform.position = camDif;
            }
        } else if (moveHorizontal < 0 && camLeft.x >= -18)
        {
            if (playerAvg < cam.pixelWidth / 2)
            {
                camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg, height / 2));
                transform.position = camDif;
            }
        }
        //transform.position = player.transform.position + offset;
    }
}
