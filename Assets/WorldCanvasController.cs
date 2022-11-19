using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasController : MonoBehaviour
{
    public int selectedWorld = 1;
    public int moveDistance;
    bool change;
    public float changeTime;
    float count;
    List<GameObject> worlds = new List<GameObject>();
    List<float> worldStartPos = new List<float>();
    bool transition;
    public float transChunk;
    public int transCount;
    int timer;
    float transDis;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        transition = false;
        change = false;
        count = 0;
        GameObject world1 = GameObject.Find("World1");
        GameObject world2 = GameObject.Find("World2");
        GameObject world3 = GameObject.Find("World3");
        GameObject world4 = GameObject.Find("World4");
        GameObject world5 = GameObject.Find("World5");
        worlds.Add(world1);
        worlds.Add(world2);
        worlds.Add(world3);
        worlds.Add(world4);
        worlds.Add(world5);
        worldStartPos.Add(world1.transform.position.x);
        worldStartPos.Add(world2.transform.position.x);
        worldStartPos.Add(world3.transform.position.x);
        worldStartPos.Add(world4.transform.position.x);
        worldStartPos.Add(world5.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Wolrd 5 pos: " + worlds[4].transform.position.x);
        float deltaDistance = 0.0f;
        if (Input.GetKey("right") && selectedWorld < 5) {
            if (!change) { 
                selectedWorld = selectedWorld + 1;
                if (selectedWorld == 3 || selectedWorld == 4) {
                    deltaDistance = worlds[selectedWorld - 1].transform.position.x - this.transform.position.x;
                    Debug.Log("World: " + selectedWorld + "  " + worlds[selectedWorld - 1].transform.position.x);
                    Debug.Log(this.transform.position.x);
                    Debug.Log(deltaDistance);
                    if (selectedWorld == 4) {
                        deltaDistance = deltaDistance * 3.0f / 4.0f;
                    }
                    transDis = -deltaDistance / transChunk;
                    transition = true;
                    for (int x = 0; x < worlds.Count; x++)
                    {
                        //worlds[x].transform.position = worlds[x].transform.position + new Vector3(-deltaDistance, 0, 0);
                    }
                    //transform.position = transform.position + new Vector3(moveDistance, 0, 0);
                }
                change = true;
                GameObject oldSelect = worlds[selectedWorld - 2];
                Animator oldAnim = oldSelect.GetComponent<Animator>();
                oldAnim.SetBool("Selected", false);
                GameObject newSelect = worlds[selectedWorld - 1];
                Animator newAnim = newSelect.GetComponent<Animator>();
                newAnim.SetBool("Selected", true);
            }
        }
        if (Input.GetKey("left") && selectedWorld > 1)
        {
            if (!change)
            {
                selectedWorld = selectedWorld - 1;
                if (selectedWorld == 2 || selectedWorld == 3)
                {
                    if (selectedWorld == 2)
                    {
                        deltaDistance = GameObject.Find("World1").transform.position.x - worldStartPos[0];
                    }
                    else
                    {
                        deltaDistance = worlds[selectedWorld - 1].transform.position.x - this.transform.position.x;
                    }
                    Debug.Log("World: " + selectedWorld + "  " + worlds[selectedWorld - 1].transform.position.x);
                    Debug.Log(this.transform.position.x);
                    Debug.Log(deltaDistance);
                    transDis = -deltaDistance / transChunk;
                    transition = true;
                    for (int x = 0; x < worlds.Count; x++)
                    {
                        if (selectedWorld == 2)
                        {
                            //worlds[x].transform.position = new Vector3(worldStartPos[x], worlds[x].transform.position.y, worlds[x].transform.position.z);
                        }
                        else {
                            //worlds[x].transform.position = worlds[x].transform.position + new Vector3(-deltaDistance, 0, 0);
                        }
                    }
                    //transform.position = transform.position + new Vector3(moveDistance, 0, 0);
                }
                change = true;
                GameObject oldSelect = worlds[selectedWorld];
                Animator oldAnim = oldSelect.GetComponent<Animator>();
                oldAnim.SetBool("Selected", false);
                GameObject newSelect = worlds[selectedWorld - 1];
                Animator newAnim = newSelect.GetComponent<Animator>();
                newAnim.SetBool("Selected", true);
            }
        }
        if (change) {
            if (count > changeTime)
            {
                count = 0;
                change = false;
            }
            count = count + Time.fixedUnscaledDeltaTime;
        }
        if (transition) {
            for (int x = 0; x < worlds.Count; x++) {
                worlds[x].transform.position = worlds[x].transform.position + new Vector3(transDis, 0, 0);
            }
            timer = timer + 1;
            if (transCount <= timer) {
                transition = false;
                timer = 0;
            }
        }
        //GameObject parentObject = transform.parent.gameObject;
        //Debug.Log("parentObject rectTransform :" + parentObject.GetComponent<RectTransform>().anchoredPosition);
        //GameObject childSelect = GameObject.Find("World" + selectedWorld);
        //Debug.Log("childSelect: " + childSelect.name);
        //Debug.Log("childObject rectTransform :" + childSelect.GetComponent<RectTransform>().anchoredPosition);
    }
}
