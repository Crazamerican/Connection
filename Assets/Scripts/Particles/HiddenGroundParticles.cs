using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGroundParticles : MonoBehaviour
{
    VerticalMaster vertMaster;

    public GameObject groundPrefab;

    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        vertMaster = GetComponent<VerticalMaster>();
    }

    void FixedUpdate()
    {
        if (vertMaster.getHiddenGroundFlag1() == true)
        {
            drawHiddenGround1();
        }
        else
        {
            Destroy(groundPrefab);
        }

        if (vertMaster.getHiddenGroundFlag2() == true)
        {
            drawHiddenGround2();
        }
        else
        {
            Destroy(groundPrefab);
        }
    }

    private void drawHiddenGround1()
    {
        Vector3 player1Pos = player1.transform.position;
        Instantiate(groundPrefab, player1Pos, Quaternion.identity);
        Destroy(groundPrefab);
    }

    private void drawHiddenGround2()
    {
        Vector3 player2Pos = player2.transform.position;
        Instantiate(groundPrefab, player2Pos, Quaternion.identity);
        Destroy(groundPrefab);
    }
}
