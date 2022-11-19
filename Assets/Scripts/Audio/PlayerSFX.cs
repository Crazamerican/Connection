using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip headBonk;

    public enum ClipName { HEAD_BONK };

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
            audioSource.PlayOneShot(headBonk, 0.35f);
        }
    }
}
