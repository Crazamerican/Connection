using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform objectRectTransform = gameObject.GetComponent<RectTransform>();
        Debug.Log("width: " + objectRectTransform.rect.width + ", height: " + objectRectTransform.rect.height);
    }
}
