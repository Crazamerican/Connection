using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningLazer : MonoBehaviour
{

    public int speed = 2;
    float timer = 0f;
    public Transform middlePoint;
    public float width = 5;
    public float height = 5;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        float x = Mathf.Cos(timer) * width;
        float y = Mathf.Sin(timer) * height;
        transform.position = new Vector3(x + middlePoint.position.x, y + middlePoint.position.y, 0f);
    }
}
