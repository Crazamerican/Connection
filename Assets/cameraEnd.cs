using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraEnd : MonoBehaviour
{
    public bool cameraHere;

    private void Start()
    {
        cameraHere = false;
    }
    void OnBecameVisible()
    {
        cameraHere = true;
    }
    private void OnBecameInvisible()
    {
        cameraHere = false;
    }
}
