using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    public GameObject player;
    VerticalMaster verticalMaster;
    // Start is called before the first frame update
    void Start()
    {
        verticalMaster = player.GetComponent<VerticalMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            verticalMaster.velocity = 0.4f;
            verticalMaster.velocity2 = 0.4f;

            verticalMaster.grounded = false;
            verticalMaster.grounded2 = false;
            verticalMaster.forgiveGround = false;
            verticalMaster.forgiveGround2 = false;
            verticalMaster.coyoteGround = false;
            verticalMaster.coyoteGround2 = false;

            verticalMaster.charAnim.ResetTrigger("grounded");
            verticalMaster.otherCharAnim.ResetTrigger("grounded");

            verticalMaster.charAnim.SetTrigger("jumped");
            verticalMaster.otherCharAnim.SetTrigger("jumped");
        }
    }
}
