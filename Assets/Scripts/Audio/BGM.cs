using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public AudioSource subduedTrack;
    public AudioSource fullTrack;

    Vector3 subduedVelocity;
    Vector3 fullVelocity;

    // Start is called before the first frame update
    void Start()
    {
        //Make sure we don't create multiple copies of BGM
        GameObject[] otherInstances = GameObject.FindGameObjectsWithTag("Music");

        if (otherInstances.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += CheckIfFadeHappens;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckIfFadeHappens(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "HubWorldMusicTest")
        {
            FadeInFull();
        }
    }

    public void FadeInFull()
    {
        StartCoroutine(BringingInFullTrack());
    }

    private IEnumerator BringingInFullTrack()
    {
        //Bring in the full track in 1.6 seconds
        while (fullTrack.volume < .32)
        {
            fullTrack.volume += .02f;
            subduedTrack.volume -= .02f;

            yield return new WaitForSecondsRealtime(.1f) ;
        }
        
    }
}
