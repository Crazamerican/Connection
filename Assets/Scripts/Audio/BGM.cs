using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioClip MainMenuAudioClip;
    public AudioClip Level1AudioClip;
    public AudioClip World2AudioClip;

    public static BGM Instance { get; private set; }

    Vector3 subduedVelocity;
    Vector3 fullVelocity;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(this.gameObject.transform.parent);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += CheckIfMusicNeedsToChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckIfMusicNeedsToChange(Scene scene, LoadSceneMode mod)
    {
        if (scene.name == "TitleCard_Demo")
        {
            BGMSource.volume = 1f;
            BGMSource.clip = MainMenuAudioClip;
            BGMSource.Play();
        }
        else if (BGMSource.clip != Level1AudioClip && (scene.name == "UILevelSelect1" || scene.name == "Level1_Cutscene"))
        {
            BGMSource.volume = .45f;
            BGMSource.clip = Level1AudioClip;
            BGMSource.Play();
        }
        else if (BGMSource.clip != World2AudioClip && scene.name == "UILevelSelect2")
        {
            BGMSource.volume = .7f;
            BGMSource.clip = World2AudioClip;
            BGMSource.Play();
        }
    }
}
