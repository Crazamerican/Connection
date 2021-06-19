using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using SpeechEnums;

public class TeleType : MonoBehaviour
{

    private TextMeshPro textMeshPro;
    private bool running;
    private string secondString;
    public GameObject bg;
    public float secondsToWait = .5f;
    public string speechType = "Bubble";
    private SpeechEnum type;
    public Sprite board;
    public Sprite bubble;
    public bool freezePlayer = false;
    private GameManagementScript manager;
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {

        textMeshPro = gameObject.GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
        textMeshPro.maxVisibleCharacters = 0;
        running = false;
        secondString = "This is the second string to Test with heheXD";
        bg.GetComponent<SpriteRenderer>().enabled = false;
        manager = GameObject.Find("EndOfLevel").GetComponent<GameManagementScript>();
        if(speechType.ToLower().Equals("bubble"))
        {
            type = SpeechEnum.Bubble;
        } else
        {
            type = SpeechEnum.Board;
        }
    }

    enum SpeechEnum
    {
        Bubble,
        Board
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void runTeleType()
    {
        if(!running)
        {
            TurnOn();
        }
        else if(!done)
        {
            StopCoroutine(WriteText());
            WriteAll();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
    {
        if(freezePlayer)
        {
             manager.freezePlayer = true;
        }
        bg.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<MeshRenderer>().enabled = true;
        running = true;
        StartCoroutine(Wait(secondsToWait));
        StartCoroutine(WriteText());
    }

    public void TurnOff()
    {
        StopCoroutine(WriteText());
        bg.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        running = false;
        if(freezePlayer)
        {
            manager.freezePlayer = false;
        }

    }

    private void WriteAll()
    {
        textMeshPro.ForceMeshUpdate();
        textMeshPro.maxVisibleCharacters = textMeshPro.textInfo.characterCount;
        done = true;
    }

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public IEnumerator WriteText()
    {
        int totalVisibleCharacters = textMeshPro.textInfo.characterCount;
        textMeshPro.ForceMeshUpdate();
        Vector2 size = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(4f, 2f);
        bg.GetComponent<SpriteRenderer>().size = size + padding;
        bg.SetActive(true);
        float speed = 0.1f;
        int counter = 0;
        done = false;
        while(!done)
        {
            if(!running)
            {
                yield break;
            }
            counter++;
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textMeshPro.maxVisibleCharacters = visibleCount;
            if(visibleCount >= totalVisibleCharacters)
            {
                done = true;
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
