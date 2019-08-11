using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePointCalculator : MonoBehaviour
{
    public Transform start;
    public Transform end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = start.position.x + (end.position.x - start.position.x) / 2;
        float y = start.position.y + (end.position.y - start.position.y) / 2;
        transform.position = new Vector3(x, y);
    }
}
