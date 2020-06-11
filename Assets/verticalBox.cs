using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalBox : MonoBehaviour
{
    public float gravity;
    public float velocity;

    float width;
    float height;
    float distToCol;
    public float bottom;
    // Start is called before the first frame update
    void Start()
    {
        velocity = 0.0f;
        width = GetComponent<BoxCollider2D>().bounds.size.x;
        height = GetComponent<BoxCollider2D>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = velocity - gravity;
        Collider2D[] bottomboi = Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity - height / 2), transform.position + new Vector3(+width / 2, velocity - height / 2 - .01f));
        distToCol = Mathf.Infinity;
        bool col = false;

        foreach (var collide in bottomboi)
        {
            if ((collide.gameObject.GetComponent<Collideable>() || collide.tag == "Ground"))
            {
                col = true;
                distToCol = 0.0f;
                velocity = 0;
                //distToCol = GetComponent<BoxCollider2D>().Distance(collide).distance;
            }
        }
        if (col == false && transform.position.y + velocity < bottom) {
            col = true;
            transform.position = new Vector3(transform.position.x, bottom);
            velocity = 0;
        }
        else if (col == false) {
            transform.position = transform.position + new Vector3(0, (velocity));
        }
    }
}
