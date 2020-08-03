using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollectibles : MonoBehaviour
{
    public bool unlock;
    public AudioClip keyGetSound;
    public AudioClip doorOpenSound;
    AudioSource audioSource;
    public GameObject otherPlayer;
    CheckCollectibles otherCollectible;
    public bool atDoor;
    private static GameObject endOfLevel;
    // Start is called before the first frame update
    void Start()
    {
        atDoor = false;
        unlock = false;
        otherCollectible = otherPlayer.GetComponent<CheckCollectibles>();
        audioSource = GetComponent<AudioSource>();
        endOfLevel = GameObject.Find("EndOfLevel");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        atDoor = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            unlock = true;
            audioSource.PlayOneShot(keyGetSound, 0.7F);
            Destroy(other.gameObject);
        }
        if (other.tag == "Trophy")
        {
            endOfLevel.GetComponent<GameManagementScript>().UpdateLevelDataTrophy();
            Destroy(other.gameObject);
        }
        if (other.tag == "Door")
        {
            unlock = endOfLevel.GetComponent<GameManagementScript>().unlock;
            atDoor = true;
            if ((unlock == true || otherCollectible.unlock == true) && atDoor == true && otherCollectible.atDoor == true)
            {
                audioSource.PlayOneShot(doorOpenSound, 0.7F);
                foreach (Transform child in other.gameObject.transform.parent.transform) {
                    Destroy(child.gameObject);
                }
                Destroy(other.gameObject.transform.parent);
                unlock = false;
                atDoor = false;
            }
        }
        
    }
}
