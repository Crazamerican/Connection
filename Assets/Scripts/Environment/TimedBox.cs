using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(BoxCollider2D))]
public class TimedBox : MonoBehaviour
{
    public float fadeTime;
    public float respawnTime;

    public BoxCollider2D collider;

    SpriteRenderer spriteRenderer;
    public TextMeshProUGUI textpro;

    private bool active = true;
    private bool playerInBounds = false;

    float velocityref;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textpro.text = fadeTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided with timed box");

        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBounds = true;
            if (active)
            {
                StartCoroutine(FadingOut());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBounds = false;
            
        }
    }

    private IEnumerator FadingOut()
    {
        float timer = 0f;
        
        

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            Color tempColor = spriteRenderer.color;
            tempColor.a = Mathf.SmoothDamp(spriteRenderer.color.a, 0f, ref velocityref, fadeTime - timer);
            spriteRenderer.color = tempColor;
            textpro.text = Mathf.FloorToInt(fadeTime - timer).ToString();
            yield return 0;
        }

        active = false;
        textpro.enabled = false;
        collider.enabled = false;

        StartCoroutine(FadingIn());
    }

    private IEnumerator FadingIn()
    {
        float timer = 0f;

        while (timer < respawnTime)
        {
            timer += Time.deltaTime;
            Color tempColor = spriteRenderer.color;
            tempColor.a = Mathf.SmoothDamp(spriteRenderer.color.a, .75f, ref velocityref, respawnTime - timer);
            spriteRenderer.color = tempColor;
            yield return 0;
        }


        //Delay reactivation of box until player is not colliding
        while (playerInBounds)
        {
            yield return 0;
        }

        active = true;
        textpro.enabled = true;
        collider.enabled = true;
        textpro.text = fadeTime.ToString();
        Color tmpColor = spriteRenderer.color;
        tmpColor.a = 1f;
        spriteRenderer.color = tmpColor;
    }
}
