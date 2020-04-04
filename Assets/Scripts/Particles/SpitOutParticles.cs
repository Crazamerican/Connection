using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitOutParticles : MonoBehaviour
{
    public Rigidbody2D particle;

    [Header("time between generating particles")]
    public float generationRate;

    [Header("horizontal distance to generate particles")]
    public float width = 20;

    [Header("number of particles in each generation")]
    public int numBurst = 3;

    // Start is called before the first frame update
    void Start()
    {
        //warmup the scene
        for (int i = 0; i < 10; i++)
        {
            Rigidbody2D p = Instantiate(particle, transform.position + new Vector3(Random.Range(-width, width), 0f, 0f),
                transform.rotation);
            p.velocity = new Vector3(0f, Random.Range(-.6f, -.4f), 0f);
            p.angularVelocity = Random.Range(-90f, 90f);
            Destroy(p.gameObject, 40);
            p.transform.localScale = new Vector3(Random.Range(.7f, 1f), Random.Range(.7f, 1f));
        }


        StartCoroutine(GenerateParticles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GenerateParticles()
    {
        while (true)
        {
            for (int i = 0; i < numBurst; i++)
            {
                Rigidbody2D p = Instantiate(particle, transform.position + new Vector3(Random.Range(-width, width), 0f, 0f),
                    transform.rotation);
                p.velocity = new Vector3(0f, Random.Range(-.6f, -.4f), 0f);
                p.angularVelocity = Random.Range(-90f, 90f);
                Destroy(p.gameObject, 40);
                p.transform.localScale = new Vector3(Random.Range(.7f, 1f), Random.Range(.7f, 1f));
            }

            yield return new WaitForSecondsRealtime(generationRate);
        }
        
    }
}
