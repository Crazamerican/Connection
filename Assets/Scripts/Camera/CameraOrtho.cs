using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrtho : MonoBehaviour
{
    public SpriteRenderer initCamSize;
    // Start is called before the first frame update
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = (initCamSize.bounds.size.x) / (initCamSize.bounds.size.y);

        if (screenRatio == targetRatio)
        {
            Camera.main.orthographicSize = (initCamSize.bounds.size.y) / 2;
        }
        else {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = (initCamSize.bounds.size.y) / 2 * differenceInSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
