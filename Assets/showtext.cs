using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class showtext : MonoBehaviour
{
    public GameObject text;
    bool atDoor = false;
    public int door;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            atDoor = true;
            text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            atDoor = false;
            text.SetActive(false);
        }
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && atDoor) {
            if (door == 1) {
                SceneManager.LoadScene("NewGraphics1");
            }
            else if (door == 2) {
                SceneManager.LoadScene("NewGraphics3");
            }
            else if (door == 3) {
                SceneManager.LoadScene("NewGraphics5");
            }
        }
    }
}
