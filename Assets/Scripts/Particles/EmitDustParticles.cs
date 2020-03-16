﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitDustParticles : MonoBehaviour
{
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmitDust()
    {
        particleSystem.Play();
    }
}
