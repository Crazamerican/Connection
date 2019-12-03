using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{

    public Vector3 direction = new Vector3(-1, 0);
    public int speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Player"))
        {
            col.gameObject.transform.position += speed * direction * Time.deltaTime;
        }
    }
}

