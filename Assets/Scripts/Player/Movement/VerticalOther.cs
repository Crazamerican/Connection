using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalOther : MonoBehaviour
{
    public bool inverted2;
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        inverted2 = false;
        width = GetComponent<BoxCollider2D>().bounds.size.x;
        height = GetComponent<BoxCollider2D>().bounds.size.y;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gravity")
        {
            inverted2 = !inverted2;
            Destroy(other.gameObject);
        }
    }

    public Collider2D[] GetTopBoi(float velocity)
    {
        return Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity + height / 2), transform.position + new Vector3(+width / 2, velocity + height / 2 + .01f));
    }

    public Collider2D[] GetBotBoi(float velocity)
    {
        return Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity - height / 2), transform.position + new Vector3(+width / 2, velocity - height / 2 - .1f));
    }

    public Collider2D[] GetBot_Forgive(float velocity)
    {
        return Physics2D.OverlapAreaAll(transform.position + new Vector3(-width / 2, velocity - height / 2), transform.position + new Vector3(+width / 2, velocity - height / 2 - .5f));
    }


}
