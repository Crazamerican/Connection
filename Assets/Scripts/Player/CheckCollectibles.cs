using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollectibles : MonoBehaviour
{
    bool unlock;
    public AudioClip keyGetSound;
    public AudioClip doorOpenSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            unlock = true;
            audioSource.PlayOneShot(keyGetSound, 0.7F);
            Destroy(other.gameObject);
        }
        if (other.tag == "Trophy")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Door")
        {
            if (unlock == true)
            {
                audioSource.PlayOneShot(doorOpenSound, 0.7F);
                Destroy(other.gameObject);
                unlock = false;
            }
        }
        
    }
}
