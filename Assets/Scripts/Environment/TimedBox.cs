using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(BoxCollider2D))]
public class TimedBox : MonoBehaviour
{
    public float fadeTime;

    public BoxCollider2D collider;

    SpriteRenderer spriteRenderer;
    public TextMeshProUGUI textpro;

    float velocityref;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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
            StartCoroutine(FadingOut());
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


        textpro.enabled = false;
        collider.enabled = false;
    }
}
