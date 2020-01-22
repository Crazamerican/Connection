using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WindTunnel : MonoBehaviour
{

    public Vector3 direction = new Vector3(-1, 0);
    public int speed = 1;
    public float rad = 5;
    ParticleSystem partySys;
    ShapeModule shape;
    public List<ParticleCollisionEvent> collisionEvents;
    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        partySys = GetComponent<ParticleSystem>();
        shape = partySys.shape; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void OnTriggerStay2D(Collider2D col)
    //{
    //    if(col.gameObject.tag.Equals("Player"))
    //    {
    //        col.gameObject.transform.position += speed * direction * Time.deltaTime;
    //    }
    //}

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit something");
            Debug.Log("Hit the player");
            int numCollisionEvents = partySys.GetCollisionEvents(other, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                other.transform.position = other.transform.position + direction * .25f;
            }
    }


}

