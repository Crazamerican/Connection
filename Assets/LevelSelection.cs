using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{

    WorldCanvasController worldController;
    public int world;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        GameObject curWorld = this.transform.parent.gameObject;
        GameObject worldThing = curWorld.transform.parent.gameObject;
        worldController = worldThing.GetComponent<WorldCanvasController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int curWorld = worldController.selectedWorld;
        int curLevel = worldController.selectedLevel;
        if (curWorld != world || curLevel != level)
        {
            Animator anim = this.GetComponent<Animator>();
            anim.SetBool("Selected", false);
        }
        else {
            Animator anim = this.GetComponent<Animator>();
            anim.SetBool("Selected", true);
        }
    }
}
