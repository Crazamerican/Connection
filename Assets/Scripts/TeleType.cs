using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleType : MonoBehaviour
{

    private TextMeshPro textMeshPro;
    private bool running;
    private string secondString;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
        textMeshPro.maxVisibleCharacters = 0;
        running = false;
        secondString = "This is the second string to Test with heheXD";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N) && !running)
        {
            running = true;
            StartCoroutine(WriteText());
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            textMeshPro.maxVisibleCharacters = 0;
            textMeshPro.text = secondString;
        }
    }

    public IEnumerator WriteText()
    {
        int totalVisibleCharacters = textMeshPro.textInfo.characterCount;
        float speed = 0.1f;
        int counter = 0;
        bool done = false;
        while(!done)
        {
            counter++;
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textMeshPro.maxVisibleCharacters = visibleCount;
            if(visibleCount >= totalVisibleCharacters)
            {
                done = true;
                running = false;
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
