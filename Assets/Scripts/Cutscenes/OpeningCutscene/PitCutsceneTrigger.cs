using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Helper script to play cutscene above pit
public class PitCutsceneTrigger : MonoBehaviour
{
    public GameObject topGirl;
    public GameObject botBoy;
    public GameManagementScript gameManager;

    public FollowPlayer cameraScript;

    bool movingCamera;

    Vector3 velocity;
    Vector3 cameraTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingCamera == true)
        {
            cameraScript.transform.position = Vector3.SmoothDamp(cameraScript.transform.position, cameraTargetPosition, ref velocity, 4f);
        }
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

        yield return new WaitForSecondsRealtime(2.5f);

        cameraTargetPosition = new Vector3(cameraScript.transform.position.x, 2.28f, cameraScript.transform.position.z);
        movingCamera = true;

        //TODO play girl's animations as well
    }
}
