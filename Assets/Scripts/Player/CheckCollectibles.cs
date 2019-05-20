using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollectibles : MonoBehaviour
{
    bool unlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            unlock = true;
            Destroy(other.gameObject);
        }
        if (other.tag == "Door")
        {
            if (unlock == true)
            {
                Destroy(other.gameObject);
                unlock = false;
            }
        }
    }
}
