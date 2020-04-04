using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedHiddenGround : MonoBehaviour
{
    void Awake()
    {
        Destroy(gameObject, 0.5F);
    }
}
