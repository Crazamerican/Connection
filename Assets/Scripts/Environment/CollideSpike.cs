using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideSpike : MonoBehaviour
{
    DeathScript deathScript;
    public GameObject playerThings;

    // Start is called before the first frame update
    void Start()
    {
        deathScript = playerThings.GetComponent<DeathScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("here!");
            deathScript.dead = true;
        }
    }
}
