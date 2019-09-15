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

    public Collider2D[] GetTop1(float velocity)
    {
        return Physics2D.OverlapCircleAll(transform.position + new Vector3(width / 2, velocity + height / 2), 0.01f);
    }

    public Collider2D[] GetTop2(float velocity)
    {
        return Physics2D.OverlapCircleAll(transform.position + new Vector3(-width / 2, velocity + height / 2), 0.01f);
    }

    public Collider2D[] GetTop3(float velocity)
    {
        return Physics2D.OverlapCircleAll(transform.position + new Vector3(0, velocity + height / 2), 0.01f);
    }

    public Collider2D[] GetBot1(float velocity)
    {
        return Physics2D.OverlapCircleAll(transform.position + new Vector3(width / 2, velocity - height / 2), 0.01f);
    }

    public Collider2D[] GetBot2(float velocity)
    {
        return Physics2D.OverlapCircleAll(transform.position + new Vector3(-width / 2, velocity - height / 2), 0.01f);
    }

    public Collider2D[] GetBot3(float velocity)
    {
        return Physics2D.OverlapCircleAll(transform.position + new Vector3(0, velocity - height / 2), 0.01f);
    }

    
}
