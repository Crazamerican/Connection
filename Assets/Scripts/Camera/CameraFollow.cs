using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;       //Public variable to store a reference to the player game object
    public float followSpeed = 1f;

    private float currentz;  //private variable to hold zpos in scene
    Vector2 velocity = Vector2.zero;

    // Use this for initialization
    void Start () 
    {
        currentz = transform.position.z;
    }
    
    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        Vector2 targetPos = Vector2.SmoothDamp(transform.position, player.transform.position, ref velocity, Time.deltaTime * followSpeed);
        transform.position = new Vector3(targetPos.x, targetPos.y, currentz);
    }
}
