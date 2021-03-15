using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInverter : MonoBehaviour
{
    public GameObject player;
    public Animator charAnim;
    VerticalMaster verticalMaster;
    bool inside;
    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        verticalMaster = player.GetComponent<VerticalMaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && inside == false)
        {
            inside = true;
            Debug.Log("thing");
            verticalMaster.gravity = verticalMaster.gravity * -1;
            verticalMaster.gravity2 = verticalMaster.gravity2 * -1;
            verticalMaster.inverted = !verticalMaster.inverted;
            verticalMaster.inverted2 = !verticalMaster.inverted2;
            if (verticalMaster.inverted) {
                verticalMaster.changeGravity = true;
                verticalMaster.velocity = 0;
                verticalMaster.velocity2 = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && inside == true)
        {
            inside = false;
            Debug.Log("thingalso");
            verticalMaster.gravity = verticalMaster.gravity * -1;
            verticalMaster.gravity2 = verticalMaster.gravity2 * -1;
            verticalMaster.inverted = !verticalMaster.inverted;
            verticalMaster.inverted2 = !verticalMaster.inverted2;
            if (verticalMaster.inverted)
            {
                verticalMaster.changeGravity = true;
                verticalMaster.velocity = 0;
                verticalMaster.velocity2 = 0;
            }
        }
    }
}
