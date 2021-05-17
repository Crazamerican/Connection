using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    public GameObject textBubble;
    public GameObject overheadSprite;
    private bool interactable;
    private TeleType script;
    public GameObject bubble;
    // Start is called before the first frame update
    void Start()
    {
        interactable = false;
        script = textBubble.GetComponent<TeleType>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable && Input.GetKeyDown(KeyCode.N))
        {
            overheadSprite.SetActive(false);

            script.TurnOn();
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.gameObject.tag.Equals("Player"))
        {
            Debug.Log("The player ran up");
            overheadSprite.SetActive(true);
            interactable = true;

        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag.Equals("Player"))
        {
            Debug.Log("The player ran away");
            overheadSprite.SetActive(false);
            interactable = false;
            script.TurnOff();
        }
    }
}
