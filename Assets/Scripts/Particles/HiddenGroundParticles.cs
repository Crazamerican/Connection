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
            vertMaster.setHiddenGroundFlag1(false);
        }

        if (vertMaster.getHiddenGroundFlag2() == true)
        {
            drawHiddenGround2();
            vertMaster.setHiddenGroundFlag2(false);
        }
    }

    private void drawHiddenGround1()
    {
        Vector3 player1Pos = player1.transform.position;
        Instantiate(groundPrefab, player1Pos + new Vector3(0.0F, -.5F, 0.0F), Quaternion.identity);
        // DestroyImmediate(groundPrefab, true);
    }

    private void drawHiddenGround2()
    {
        Vector3 player2Pos = player2.transform.position;
        Instantiate(groundPrefab, player2Pos + new Vector3(0.0F, -.5F, 0.0F), Quaternion.identity);
        // DestroyImmediate(groundPrefab, true);
    }
}
