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

    BoxCollider2D collider;
    public bool collided;

    SpriteRenderer spriteRenderer;
    public TextMeshProUGUI textpro;

    public bool active = true;
    public bool playerInBounds = false;

    public Animator anim;

    float velocityref;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textpro.text = fadeTime.ToString();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with timed box");

        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBounds = true;
            if (active && !collided)
            {
                collided = true;
                anim.SetBool("fullyrecovered", false);
                //anim.enabled = true;
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

    public void CallFadeIn()
    {
        active = false;
        textpro.enabled = false;
        collider.enabled = false;
        anim.SetBool("break", false);

        StartCoroutine(FadingIn());
    }

    private IEnumerator FadingOut()
    {
        float timer = 0f;
        anim.SetBool("crack", true);


        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            Color tempColor = spriteRenderer.color;
            tempColor.a = Mathf.SmoothDamp(spriteRenderer.color.a, .8f, ref velocityref, fadeTime - timer);
            spriteRenderer.color = tempColor;
            


            textpro.text = Mathf.FloorToInt(fadeTime - timer).ToString();
            yield return 0;
        }
        anim.SetBool("crack", false);
        //anim.ResetTrigger("crumble");
        anim.SetBool("break", true);

    }

    private IEnumerator FadingIn()
    {
        //anim.ResetTrigger("break");
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

        
        anim.SetBool("fullyrecovered", true);
        //anim.enabled = false;
        yield return new WaitForSecondsRealtime(.2f);
        active = true;
        textpro.enabled = true;
        collider.enabled = true;
        collided = false;
        textpro.text = fadeTime.ToString();
        Color tmpColor = spriteRenderer.color;
        tmpColor.a = 1f;
        spriteRenderer.color = tmpColor;
        
    }
}
