using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNameScript : MonoBehaviour
{
    bool turnOn = true;
    bool fullyOn;
    bool turnOff;
    public GameObject thisGameObject;
    float timer;
    Color32 text;
    Color32 outline;
    byte helper;
    // Start is called before the first frame update
    void Start()
    {
        fullyOn = false;
        turnOff = false;
        turnOn = true;
        if(thisGameObject != null)
        {
            text = thisGameObject.GetComponent<TextMeshProUGUI>().faceColor;
            outline = thisGameObject.GetComponent<TextMeshProUGUI>().outlineColor;
            helper = 0;
            thisGameObject.GetComponent<TextMeshProUGUI>().faceColor = new Color32(text.r, text.g, text.b, helper);
            thisGameObject.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(text.r, text.g, text.b, helper);
        }
        timer = 0.0f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(thisGameObject != null)
        {
            if(turnOn)
            {
                helper += 5;
                thisGameObject.GetComponent<TextMeshProUGUI>().faceColor = new Color32(text.r, text.g, text.b, helper);
                thisGameObject.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(text.r, text.g, text.b, helper);
                if (helper >= 254)
                {
                    turnOn = false;
                    fullyOn = true;
                }
            }
            if (fullyOn)
            {
                timer += 1.0f;
                if(timer > 120)
                {
                    fullyOn = false;
                    turnOff = true;
                }
            }
            if (turnOff)
            {
                helper -= 5;
                thisGameObject.GetComponent<TextMeshProUGUI>().faceColor = new Color32(text.r, text.g, text.b, helper);
                thisGameObject.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(text.r, text.g, text.b, helper);
                if (helper < 1)
                {
                    turnOff = false;
                    thisGameObject.SetActive(false);
                }
            }
        }
    }
}
