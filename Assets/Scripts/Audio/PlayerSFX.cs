using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip headBonk;
    public AudioClip jump;

    public enum ClipName { HEAD_BONK, JUMP };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playPlayerSFX(ClipName clip)
    {
        if (clip == ClipName.HEAD_BONK)
        {
            audioSource.PlayOneShot(headBonk, VaryIntensity(0.30f, 0.02f));
        }
        else if (clip == ClipName.JUMP)
        {
            audioSource.PlayOneShot(jump, VaryIntensity(0.5f, .05f));
        }
    }

    private float VaryIntensity(float value, float variance)
    {

        return value + Random.Range(-variance, variance);
    }
}
