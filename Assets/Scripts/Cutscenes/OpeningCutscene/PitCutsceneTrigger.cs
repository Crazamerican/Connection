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
    float movingCameraTimer;

    Vector3 velocity;
    Vector3 cameraTargetPosition;
    float blockingAlpha;

    bool cutsceneTriggered;

    public SpriteRenderer blockingSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("EndOfLevel").GetComponent<GameManagementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingCamera == true)
        {
            cameraScript.transform.position = Vector3.SmoothDamp(cameraScript.transform.position, cameraTargetPosition, ref velocity, 2.5f);
            Color blockingColor = blockingSprite.color;
            blockingSprite.color = new Color(blockingColor.r, blockingColor.g, blockingColor.b, Mathf.SmoothDamp(blockingColor.a, 0, ref blockingAlpha, 3f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pit cutscene hit");
        if (!cutsceneTriggered && collision.CompareTag("Player"))
        {
            cutsceneTriggered = true;
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

        //TODO: smooth this to zero
        verticalController.velocity = 0;
        verticalController.velocity2 = 0;
        verticalController.gravity2 = 0f;
        verticalController.gravity = 0f;

        botBoy.GetComponentInChildren<Animator>().Play("OpeningCutscene");
        topGirl.GetComponentInChildren<Animator>().Play("OpeningCutscene");
        yield return new WaitForSecondsRealtime(4.5f);

        //resume boy's fall
        //verticalController.velocity = -.1f;
        //verticalController.velocity2 = -.1f;
        verticalController.gravity2 = .005f;
        verticalController.gravity = .005f;

        yield return new WaitForSecondsRealtime(2.5f);

        cameraTargetPosition = new Vector3(cameraScript.transform.position.x, 2.28f, cameraScript.transform.position.z);
        movingCamera = true;

        //TODO play girl's animations as well

        yield return new WaitForSecondsRealtime(13f); //TODO: get the right timing

        movingCamera = false;
        gameManager.freezePlayer = false;


        verticalController.gravity2 = originalBoyGrav;
        verticalController.gravity = originalGirlGrav;
        botBoy.GetComponentInChildren<Animator>().Play("Idle");
        topGirl.GetComponentInChildren<Animator>().Play("Idle");
        ParticleSystem ps = topGirl.GetComponentInChildren<ParticleSystem>(); 
        var em = ps.emission; //idk why you have to do it like this but you do
        em.enabled = true; //reenable walk particles

    }
}
