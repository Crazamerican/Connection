using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Helper script to play cutscene above pit
public class PitCutsceneTrigger : MonoBehaviour
{
    public GameObject topGirl;
    public GameObject botBoy;
    public GameManagementScript gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pit cutscene hit");
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PitCutscene());
            
        }
    }

    IEnumerator PitCutscene()
    {
        gameManager.freezePlayer = true;

        VerticalMaster verticalController = topGirl.GetComponent<VerticalMaster>();

        //pause the boy's gravity so he floats above the pit
        float originalBoyGrav = verticalController.gravity2;
        float originalGirlGrav = verticalController.gravity;
        verticalController.gravity2 = 0f;
        verticalController.gravity = 0f;

        botBoy.GetComponentInChildren<Animator>().Play("OpeningCutscene");
        yield return new WaitForSecondsRealtime(4.5f);

        //resume boy's fall
        verticalController.gravity2 = originalBoyGrav;
        verticalController.gravity = originalGirlGrav;

        //TODO play girl's animations as well
    }
}
