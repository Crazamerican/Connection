using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningLazer : MonoBehaviour
{

    public int speed = 2;
    public string direction;
    float timer = 0f;
    public Transform middlePoint;
    // Start is called before the first frame update
    void Start()
    {
        if(!"right".Equals(direction.ToLower()))
        {
            speed = -speed;
        }
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        float x = Mathf.Cos(timer) * ((transform.position.x + middlePoint.position.x) / 2);
        float y = Mathf.Sin(timer) * ((transform.position.y + middlePoint.position.y) / 2);
        transform.position = new Vector3(x + middlePoint.position.x, y + middlePoint.position.y, 0f);
    }
}
