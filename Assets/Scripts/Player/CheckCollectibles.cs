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
    public bool curAtWait = false;
    public bool hitCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        hitCheckpoint = false;
        curAtWait = false;
        atDoor = false;
        unlock = false;
        otherCollectible = otherPlayer.GetComponent<CheckCollectibles>();
        audioSource = GetComponent<AudioSource>();
        endOfLevel = GameObject.Find("EndOfLevel");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitCheckpoint = false;
        atDoor = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            Debug.Log("thing");
            unlock = true;
            audioSource.PlayOneShot(keyGetSound, 0.7F);
            other.GetComponent<KeyRespawn>().collected = true;
            //Destroy(other.gameObject);
        }
        if (other.tag == "Trophy")
        {
            endOfLevel.GetComponent<GameManagementScript>().UpdateLevelDataTrophy();
            //Destroy(other.gameObject);
        }
        if (other.tag == "Door")
        {
            //unlock = endOfLevel.GetComponent<GameManagementScript>().unlock;
            atDoor = true;
            //if (!curAtWait && (unlock == true && otherCollectible.unlock == true) && atDoor == true && otherCollectible.atDoor == true)
            if ((!curAtWait && (unlock == true && otherCollectible.unlock == true) && atDoor == true && otherCollectible.atDoor == true) || (otherCollectible.curAtWait && !curAtWait))
            {
                audioSource.PlayOneShot(doorOpenSound, 0.4F);
                /*
                foreach (Transform child in other.gameObject.transform.parent.transform) {
                    Destroy(child.gameObject);
                }*/
                //Destroy(other.gameObject.transform.parent.gameObject);
                Animator anim = other.GetComponent<Animator>();
                anim.SetTrigger("isOpen");
                StartCoroutine (WaitAnim(anim, other));
                //while (!anim.GetCurrentAnimatorStateInfo(0).IsName("NoDoor")) {
                //}
                //Destroy(other.gameObject);
            }
        }
        
    }
    IEnumerator WaitAnim(Animator anim, Collider2D other) {
        curAtWait = true;
        //Debug.Log("thing: " + anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"));
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(2);
        Destroy(other.gameObject);
        unlock = false;
        atDoor = false;
        curAtWait = false;
    }
}
